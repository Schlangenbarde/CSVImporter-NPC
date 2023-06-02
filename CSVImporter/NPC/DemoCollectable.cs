using System;
using UnityEngine;

namespace CSVImporter.Npc
{
    public class DemoCollectable : MonoBehaviour
    {
        public static Action OnCollectItem;

        public void OnCollectClicked()
        {
            OnCollectItem?.Invoke();
        }
    }
}
