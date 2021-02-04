using System.Collections.Generic;

using RimWorld;
using Verse;
using Verse.AI;

namespace CM_Custom_Tools.Comps
{
    public class CompProperties_IncreaseUserHediffSeverityOnHit : CompProperties
    {
        public HediffDef hediffDef;

        public float severityPerHit;

        public CompProperties_IncreaseUserHediffSeverityOnHit()
        {
            compClass = typeof(CompIncreaseUserHediffSeverityOnHit);
        }
    }
}
