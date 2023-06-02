using System;
using UnityEngine;

namespace CSVImporter.Npc
{
    public class DemoKillable : MonoBehaviour
    {
        public static Action OnKillItem;

        public void OnKillClicked()
        {
            OnKillItem?.Invoke();
        }
    }
}