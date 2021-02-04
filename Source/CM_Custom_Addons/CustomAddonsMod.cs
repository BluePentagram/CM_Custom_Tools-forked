using UnityEngine;
using HarmonyLib;
using RimWorld;
using Verse;

namespace CM_Custom_Addons
{
    public class CustomAddonsMod : Mod
    {
        private static CustomAddonsMod _instance;
        public static CustomAddonsMod Instance => _instance;

        public CustomAddonsMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("CM_Custom_Addons");
            harmony.PatchAll();

            _instance = this;
        }
    }
}
