using CSVImporter;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CSVImporter.Npc
{
    public class QuestLog : MonoBehaviour
    {
        [SerializeField]
        private QuestLogTemplate template;

        private Dictionary<BaseImportObject, QuestLogTemplate> activeQuests = new Dictionary<BaseImportObject, QuestLogTemplate>();

        public static Action<GameObject> OnQuestFinished;


        private void OnEnable()
        {
            NPC.OnQuestStarted += CreateNewQuestLogItem;
            NPC.OnQuestFinished += DeleteQuestLogItem;

            SubscribeToNPCEvents();
        }

        private void OnDisable()
        {
            NPC.OnQuestStarted -= CreateNewQuestLogItem;
            NPC.OnQuestFinished -= DeleteQuestLogItem;

            UnsubscribeToNPCEvents();
        }

        private void CreateNewQuestLogItem(BaseImportObject data)
        {
            var obj = Instantiate(template, transform);
            activeQuests.Add(data, obj);
            obj.SetupDemoData((DemoData)data);
        }

        private void DeleteQuestLogItem(BaseImportObject data)
        {
            if (!activeQuests.ContainsKey(data)) return;

            OnQuestFinished?.Invoke(activeQuests[data]?.gameObject);
            Destroy(activeQuests[data].gameObject);
            activeQuests.Remove(data);
        }

        private void UpdateQuestLogItem(DemoData data, int amount)
        {
            if (!activeQuests.ContainsKey(data)) return;

            activeQuests[data].UpdateData(data, amount);
        }

        private void UpdateQuestLogItem(DemoData data, bool didMeet)
        {
            if (!activeQuests.ContainsKey(data)) return;
            activeQuests[data].UpdateData(data, didMeet);
        }

        private void UpdateQuestLogItem()
        {

        }

        private void SubscribeToNPCEvents()
        {
            CollectNpc.OnUpdateQuestLog += UpdateQuestLogItem;
            KillNpc.OnUpdateQuestLog += UpdateQuestLogItem;
            MeetNpc.OnUpdateQuestLog += UpdateQuestLogItem;
        }

        private void UnsubscribeToNPCEvents()
        {
            CollectNpc.OnUpdateQuestLog -= UpdateQuestLogItem;
            KillNpc.OnUpdateQuestLog -= UpdateQuestLogItem;
            MeetNpc.OnUpdateQuestLog -= UpdateQuestLogItem;
        }
    }
}