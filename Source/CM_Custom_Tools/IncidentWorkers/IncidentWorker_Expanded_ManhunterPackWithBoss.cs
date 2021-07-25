using System.Collections.Generic;
using RimWorld;
using Verse;

namespace CM_Custom_Tools.IncidentWorkers
{
    public class IncidentWorker_Expanded_ManhunterPackWithBoss : IncidentWorker
    {
        protected const float PointsFactor = 1f;

        protected const int AnimalsStayDurationMin = 60000;

        protected const int AnimalsStayDurationMax = 120000;

        protected virtual PawnKindDef GetAnimalKind(float points, int tile)
        {
            PawnKindDef result = null;

            if (result == null && def is IncidentDefExpanded)
            {
                List<PawnKindDef> pawnKindDefs = (def as IncidentDefExpanded).pawnKinds;

                if (pawnKindDefs != null && pawnKindDefs.Count > 0)
                {
                    result = pawnKindDefs.RandomElement();
                }
            }

            if (result == null)
            {
                ManhunterPackIncidentUtility.TryFindManhunterAnimalKind(points, tile, out result);
            }

            return result;
        }

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }
            Map map = (Map)parms.target;
            IntVec3 result;
            return RCellFinder.TryFindRandomPawnEntryCell(out result, map, CellFinder.EdgeRoadChance_Animal);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef bossKind = def.pawnKind;
            PawnKindDef animalKind = GetAnimalKind(parms.points, map.Tile);

            if (bossKind == null || animalKind == null || ManhunterPackIncidentUtility.GetAnimalsCount(animalKind, (parms.points * PointsFactor) - bossKind.combatPower) <= 0)
            {
                return false;
            }
            IntVec3 result = parms.spawnCenter;
            if (!result.IsValid && !RCellFinder.TryFindRandomPawnEntryCell(out result, map, CellFinder.EdgeRoadChance_Animal))
            {
                return false;
            }

            List<Pawn> list = ManhunterPackIncidentUtility.GenerateAnimals(bossKind, map.Tile, bossKind.combatPower, 1);
            list.AddRange(ManhunterPackIncidentUtility.GenerateAnimals(animalKind, map.Tile, (parms.points * PointsFactor) - bossKind.combatPower, parms.pawnCount));
            Rot4 rot = Rot4.FromAngleFlat((map.Center - result).AngleFlat);
            for (int i = 0; i < list.Count; i++)
            {
                Pawn pawn = list[i];
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(result, map, 10);
                QuestUtility.AddQuestTag(GenSpawn.Spawn(pawn, loc, map, rot), parms.questTag);
                pawn.health.AddHediff(HediffDefOf.Scaria);
                pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent);
                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + Rand.Range(AnimalsStayDurationMin, AnimalsStayDurationMax);
            }

            TaggedString labelString = null;
            if (def.letterLabel != null)
                labelString = def.letterLabel;
            else
                labelString = "LetterLabelManhunterPackArrived".Translate();

            TaggedString textString = null;
            if (def.letterText != null)
                textString = def.letterText;
            else
                textString = "ManhunterPackArrived".Translate(animalKind.GetLabelPlural());

            SendStandardLetter(labelString, textString, LetterDefOf.ThreatBig, parms, list[0]);
            Find.TickManager.slower.SignalForceNormalSpeedShort();
            LessonAutoActivator.TeachOpportunity(ConceptDefOf.ForbiddingDoors, OpportunityType.Critical);
            LessonAutoActivator.TeachOpportunity(ConceptDefOf.AllowedAreas, OpportunityType.Important);
            return true;
        }
    }

}
