using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace CM_Custom_Tools.Comps
{
    [StaticConstructorOnStartup]
    public class CompIncreaseUserHediffSeverityOnHit : ThingComp
    {
        public CompProperties_IncreaseUserHediffSeverityOnHit Props => props as CompProperties_IncreaseUserHediffSeverityOnHit;

        public override void Notify_UsedWeapon(Pawn pawn)
        {
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediffDef);

            if (hediff == null)
                hediff = pawn.health.AddHediff(Props.hediffDef);

            hediff.Severity = hediff.Severity + Props.severityPerHit;
        }
    }
}
