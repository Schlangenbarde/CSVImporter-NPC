using System;
using UnityEngine;

namespace CSVImporter.Npc
{
    public class MeetNpc : NPC
    {
        [SerializeField]
        private NPC personToMeet;

        public static Action<DemoData, bool> OnUpdateQuestLog;

        private bool didMeetPerson;

        private void OnEnable()
        {
            if (isQuestActive && !isQuestFinished) personToMeet.OnPlayerInteract += OnPlayerMeet;
        }

        private void OnDisable()
        {
            if (isQuestActive) DisableAllActiveEvents();
        }

        protected override void DisableAllActiveEvents()
        {
            personToMeet.OnPlayerInteract -= OnPlayerMeet;
        }

        protected override void InteractAfterQuest()
        {
            if (data.GetType() != typeof(DemoData)) return;

            Debug.Log($"{((DemoData)data).name}: thank you");
        }

        protected override void InteractWhileQuest()
        {
            if (data.GetType() != typeof(DemoData)) return;

            Debug.Log($"{((DemoData)data).name}: Did you meet {personToMeet.name}?");
        }

        protected override bool IsQuestFinished()
        {
            return didMeetPerson;
        }

        protected override void StartQuest()
        {
            personToMeet.OnPlayerInteract += OnPlayerMeet;
        }

        protected override void UpdateQuest()
        {
            if (data.GetType() != typeof(DemoData)) return;

            OnUpdateQuestLog?.Invoke((DemoData)data, IsQuestFinished());
        }

        private void OnPlayerMeet()
        {
            didMeetPerson = true;
            UpdateQuest();
        }


    }
}