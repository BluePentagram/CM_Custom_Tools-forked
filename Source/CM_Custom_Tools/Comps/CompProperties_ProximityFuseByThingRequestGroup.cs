using System.Collections.Generic;

using RimWorld;
using Verse;
using Verse.AI;

namespace CM_Custom_Tools.Comps
{
    public class CompProperties_ProximityFuseByThingRequestGroup : CompProperties
    {
        public ThingRequestGroup group;

        public float radius;

        public CompProperties_ProximityFuseByThingRequestGroup()
        {
            compClass = typeof(CompProximityFuseByThingRequestGroup);
        }

        public override IEnumerable<string> ConfigErrors(ThingDef parentDef)
        {
            foreach (string item in base.ConfigErrors(parentDef))
            {
                yield return item;
            }
            if (parentDef.tickerType != TickerType.Normal)
            {
                yield return string.Concat("CompProximityFuseByThingRequestGroup needs tickerType ", TickerType.Rare, " or faster, has ", parentDef.tickerType);
            }
            if (parentDef.CompDefFor<CompExplosive>() == null)
            {
                yield return "CompProximityFuseByThingRequestGroup requires a CompExplosive";
            }
        }
    }
}
