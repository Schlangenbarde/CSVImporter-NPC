using System;
using System.Linq;
using UnityEngine;

namespace CSVImporter.Npc
{
    public class CollectNpc : NPC
    {
        private int currentCollectedAmount;
        public static Action<DemoData, int> OnUpdateQuestLog;

        private void OnEnable()
        {
            if (isQuestActive && !isQuestFinished) DemoCollectable.OnCollectItem += UpdateQuest;
        }

        private void OnDisable()
        {
            if (isQuestActive) DisableAllActiveEvents();
        }


        protected override void StartQuest()
        {
            DemoCollectable.OnCollectItem += UpdateQuest;
        }

        protected override void InteractWhileQuest()
        {
            if (data.GetType() != typeof(DemoData)) return;

            Debug.Log($"{((DemoData)data).name}: Did you collect all {((DemoData)data).amount}?");
        }

        protected override bool IsQuestFinished()
        {
            if (data.GetType() != typeof(DemoData)) return false;

            return ((DemoData)data).amount <= currentCollectedAmount;

        }

        protected override void UpdateQuest()
        {
            currentCollectedAmount++;
            OnUpdateQuestLog?.Invoke((DemoData)data, currentCollectedAmount);
        }

        protected override void DisableAllActiveEvents()
        {
            DemoCollectable.OnCollectItem -= UpdateQuest;
        }

        protected override void InteractAfterQuest()
        {
            if (data.GetType() != typeof(DemoData)) return;
            Debug.Log($"{((DemoData)data).name}: Thanks for helping me");
        }
    }

}