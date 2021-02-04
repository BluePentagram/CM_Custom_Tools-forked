using System.Collections.Generic;

using RimWorld;
using Verse;
using Verse.AI;

namespace CM_Custom_Addons.Comps
{
    public class CompProperties_IncreaseUserHediffSeverityOnHit : CompProperties
    {
        public HediffDef hediff;

        public float severityPerHit;

        public CompProperties_IncreaseUserHediffSeverityOnHit()
        {
            compClass = typeof(CompIncreaseUserHediffSeverityOnHit);
        }
    }
}
