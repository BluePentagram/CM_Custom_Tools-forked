using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

using CM_Custom_Tools.Comps;

namespace CM_Custom_Tools.Patches
{
    [StaticConstructorOnStartup]
    public static class Pawn_Patches
    {
        [HarmonyPatch(typeof(Pawn))]
        [HarmonyPatch("PostApplyDamage", MethodType.Normal)]
        public static class Pawn_PostApplyDamage
        {
            [HarmonyPostfix]
            public static void Postfix(Pawn __instance, DamageInfo dinfo, float totalDamageDealt)
            {
                if (__instance == null)
                    return;

                if (__instance.apparel != null)
                {
                    for (int i = 0; i < __instance.apparel.WornApparel.Count; ++i)
                    {
                        Apparel apparel = __instance.apparel.WornApparel[i];
                        CompIncreaseUserHediffSeverityWhenHurt comp = apparel.GetComp<CompIncreaseUserHediffSeverityWhenHurt>();

                        if (comp != null)
                            comp.PostPostApplyDamage(dinfo, totalDamageDealt);
                    }
                }

                if (__instance.equipment != null)
                {
                    for (int i = 0; i < __instance.equipment.AllEquipmentListForReading.Count; ++i)
                    {
                        ThingWithComps equipped = __instance.equipment.AllEquipmentListForReading[i];
                        CompIncreaseUserHediffSeverityWhenHurt comp = equipped.GetComp<CompIncreaseUserHediffSeverityWhenHurt>();

                        if (comp != null)
                            comp.PostPostApplyDamage(dinfo, totalDamageDealt);
                    }
                }

                
            }
        }
    }
}
