using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace CM_Custom_Addons.Comps
{
    [StaticConstructorOnStartup]
    public class CompIncreaseUserHediffSeverityWhenHurt : ThingComp
    {
        public CompProperties_IncreaseUserHediffSeverityWhenHurt Props => props as CompProperties_IncreaseUserHediffSeverityWhenHurt;

        public float lastHealthValue = 1.0f;

        private Pawn User
        {
            get
            {
                if (base.ParentHolder is Pawn_ApparelTracker)
                    return (base.ParentHolder as Pawn_ApparelTracker).pawn;
                if (base.ParentHolder is Pawn_EquipmentTracker)
                    return (base.ParentHolder as Pawn_EquipmentTracker).pawn;

                return null;
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(ref lastHealthValue, "lastHealthValue", 1.0f);
        }

        public override void Notify_Equipped(Pawn pawn)
        {
            base.Notify_Equipped(pawn);

            lastHealthValue = pawn.health?.summaryHealth?.SummaryHealthPercent ?? 1.0f;
        }

        public override void PostPostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            base.PostPostApplyDamage(dinfo, totalDamageDealt);

            DoEffect();
        }

        private void DoEffect()
        {
            Pawn user = this.User;

            float health = user?.health?.summaryHealth?.SummaryHealthPercent ?? 1.0f;

            if (user == null || user.health == null)
            {
                Log.Message("No user or no user.health");
                lastHealthValue = health;
                return;
            }

            float healthChange = health - lastHealthValue;
            float hediffSeverityChange = (healthChange * -Props.severityToHealthFactor);
            lastHealthValue = health;

            if (healthChange == 0.0f || hediffSeverityChange == 0.0f)
            {
                //Log.Message("Health or severity change is zero");
                return;
            }

            Hediff hediff = user.health.hediffSet.GetFirstHediffOfDef(Props.hediff);

            if (hediff == null)
            {
                if (Props.hediff != null)
                {
                    //Log.Message("Adding hediff: " + Props.hediff + " with severity: " + hediffSeverityChange);
                    hediff = user.health.AddHediff(Props.hediff);
                    hediff.Severity = hediffSeverityChange;
                }
                else
                {
                    //Log.Message("Null hediff requested");
                    return;
                }
            }
            else
            {
                //Log.Message("Hediff severity changing by: " + hediffSeverityChange);
                hediff.Severity = hediff.Severity + hediffSeverityChange;
            }
        }
    }
}
