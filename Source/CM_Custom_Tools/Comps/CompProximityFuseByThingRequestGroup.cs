using RimWorld;
using Verse;
using Verse.AI;

namespace CM_Custom_Tools.Comps
{
    [StaticConstructorOnStartup]
    public class CompProximityFuseByThingRequestGroup : ThingComp
    {
        public CompProperties_ProximityFuseByThingRequestGroup Props => props as CompProperties_ProximityFuseByThingRequestGroup;

        public override void CompTick()
        {
            if (Find.TickManager.TicksGame % 250 == 0)
            {
                CompTickRare();
            }
        }

        public override void CompTickRare()
        {
            //if (GenClosest.ClosestThingReachable(parent.Position, parent.Map, ThingRequest.ForDef(Props.target), PathEndMode.OnCell, TraverseParms.For(TraverseMode.NoPassClosedDoors), Props.radius) != null)
            if (GenClosest.ClosestThingReachable(parent.Position, parent.Map, ThingRequest.ForGroup(Props.group), PathEndMode.OnCell, TraverseParms.For(TraverseMode.NoPassClosedDoors), Props.radius) != null)
            {
                parent.GetComp<CompExplosive>().StartWick();
            }
        }
    }
}
