using System.Collections.Generic;
using System.Linq;

using RimWorld;
using Verse;

namespace CM_Custom_Tools.IngestionOutcomeDoers
{
    public class IngestionOutcomeDoer_ChangeHediffSeverity : IngestionOutcomeDoer
    {
        public HediffDef hediffDef;

        public float severity = -1f;

        public bool isScalar = false;

        public ChemicalDef toleranceChemical;

        public bool divideByBodySize;

        public bool applyGeneToleranceFactor;

        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs.Where(hd => hd.def == hediffDef).ToList();

            foreach (Hediff hediff in hediffs)
            {
                float newSeverity = hediff.Severity;

                float effect = severity;

                if (divideByBodySize)
                    effect /= pawn.BodySize;

                AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize(pawn, toleranceChemical, ref effect, applyGeneToleranceFactor);

                if (isScalar)
                    newSeverity *= effect;
                else
                    newSeverity += effect;

                hediff.Severity = newSeverity;
            }
        }
    }
}
