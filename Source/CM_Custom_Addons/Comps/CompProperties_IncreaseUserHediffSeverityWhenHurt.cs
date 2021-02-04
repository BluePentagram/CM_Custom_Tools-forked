using System.Collections.Generic;

using RimWorld;
using Verse;
using Verse.AI;

namespace CM_Custom_Addons.Comps
{
    public class CompProperties_IncreaseUserHediffSeverityWhenHurt : CompProperties
    {
        public HediffDef hediff;

        public float severityToHealthFactor;

        public CompProperties_IncreaseUserHediffSeverityWhenHurt()
        {
            compClass = typeof(CompIncreaseUserHediffSeverityWhenHurt);
        }
    }
}
