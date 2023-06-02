using CSVImporter;
using System;
using UnityEngine;

namespace CSVImporter.Npc
{
    public class NPC : MonoBehaviour
    {
        [SerializeField]
        protected BaseImportObject data;

        public static Action<BaseImportObject> OnQuestStarted;
        public static Action<BaseImportObject> OnQuestFinished;

        public Action OnPlayerInteract;

        protected bool isQuestActive;
        protected bool isQuestFinished;

        public void Interact()
        {
            OnPlayerInteract?.Invoke();

            if (isQuestFinished)
            {
                InteractAfterQuest();
                return;
            }

            if (isQuestActive)
            {
                _EndQuest();
                return;
            }

            _StartQuest();
        }

        private void _StartQuest()
        {
            OnQuestStarted?.Invoke(data);
            isQuestActive = true;
            StartQuest();
        }

        protected virtual void StartQuest()
        {

        }


        private void _InteractWhileQuest()
        {
            if (isQuestActive && !isQuestFinished) return;
            InteractWhileQuest();
        }

        protected virtual void InteractWhileQuest()
        {
        }

        protected virtual bool IsQuestFinished()
        {
            return false;
        }

        private void _EndQuest()
        {
            if (IsQuestFinished())
            {
                OnQuestFinished?.Invoke(data);
                isQuestFinished = true;
                isQuestActive = false;
                DisableAllActiveEvents();
                return;
            }

            _InteractWhileQuest();
        }

        protected virtual void InteractAfterQuest()
        {

        }

        protected virtual void UpdateQuest()
        {

        }

        protected virtual void DisableAllActiveEvents()
        {

        }
    }
}