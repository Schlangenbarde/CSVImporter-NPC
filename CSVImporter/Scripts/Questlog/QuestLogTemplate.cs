using UnityEngine;
using UnityEngine.UI;

namespace CSVImporter.Npc
{
    public class QuestLogTemplate : MonoBehaviour
    {
        [SerializeField]
        private Text questTypeField;

        [SerializeField]
        private Text numberField;

        public void SetupDemoData(DemoData data)
        {
            questTypeField.text = data.type.ToString();
            if (data.type == QuestType.COLLECT || data.type == QuestType.KILL)
            {
                numberField.text = $"0 / {data.amount}";

            }
            else if (data.type == QuestType.MEET)
            {
                numberField.text = "NO";
            }
        }

        public void UpdateData(DemoData data, int amount)
        {
            numberField.text = $"{amount} / {data.amount}";
        }

        public void UpdateData(DemoData data, bool didMeet)
        {
            numberField.text = $"{didMeet}";
        }

        public void OnQuestFinshed()
        {
            GetComponent<Image>().color = new Color(200, 200, 200);
            questTypeField.color = new Color(212, 212, 212);
            numberField.color = new Color(212, 212, 212);
        }
    }
}