using System;
using UnityEngine;


namespace CSVImporter.Npc
{
    public class FinishedQuestLog : MonoBehaviour
    {
        private void OnEnable()
        {
            QuestLog.OnQuestFinished += CreateNewLogItem;
        }

        private void OnDisable()
        {
            QuestLog.OnQuestFinished -= CreateNewLogItem;
        }

        private void CreateNewLogItem(GameObject obj)
        {
            Instantiate(obj, transform);
            if (obj.GetComponent<QuestLogTemplate>()) obj.GetComponent<QuestLogTemplate>().OnQuestFinshed();
        }
    }
}