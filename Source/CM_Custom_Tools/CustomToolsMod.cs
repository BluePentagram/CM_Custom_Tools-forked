using UnityEngine;
using HarmonyLib;
using RimWorld;
using Verse;

namespace CM_Custom_Tools
{
    public class CustomToolsMod : Mod
    {
        private static CustomToolsMod _instance;
        public static CustomToolsMod Instance => _instance;

        public CustomToolsMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("CM_Custom_Tools");
            harmony.PatchAll();

            _instance = this;
        }
    }
}
