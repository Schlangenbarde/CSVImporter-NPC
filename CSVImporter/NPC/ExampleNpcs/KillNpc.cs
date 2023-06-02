using System;
using UnityEngine;

namespace CSVImporter.Npc
{
    public class KillNpc : NPC
    {
        private int currentKilledAmount;
        public static Action<DemoData, int> OnUpdateQuestLog;

        private void OnEnable()
        {
            if (isQuestActive && !isQuestFinished) DemoKillable.OnKillItem += UpdateQuest;
        }

        private void OnDisable()
        {
            if (isQuestActive) DisableAllActiveEvents();
        }

        protected override void StartQuest()
        {
            DemoKillable.OnKillItem += UpdateQuest;
        }

        protected override void InteractWhileQuest()
        {
            if (data.GetType() != typeof(DemoData)) return;

            Debug.Log($"{((DemoData)data).name}: Did you kill all {((DemoData)data).amount}?");
        }

        protected override bool IsQuestFinished()
        {
            if (data.GetType() != typeof(DemoData)) return false;

            return ((DemoData)data).amount <= currentKilledAmount;
        }

        protected override void UpdateQuest()
        {
            currentKilledAmount++;
            OnUpdateQuestLog?.Invoke((DemoData)data, currentKilledAmount);
        }

        protected override void DisableAllActiveEvents()
        {
            DemoKillable.OnKillItem -= UpdateQuest;
        }

        protected override void InteractAfterQuest()
        {
            if (data.GetType() != typeof(DemoData)) return;

            Debug.Log($"{((DemoData)data).name}: Thanks for helping me");
        }
    }
}