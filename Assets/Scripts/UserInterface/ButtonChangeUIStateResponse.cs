using System.Collections.Generic;
using UnityEngine;

namespace UserInterface
{
    public class ButtonChangeUIStateResponse : MonoBehaviour, IButton
    {
        [SerializeField] private List<GameObject> objectsToActivate;
        [SerializeField] private List<GameObject> objectsToDeactivate;

        public void ExecuteButtonFunctionality()
        {
            ChangeUIState();
        }

        private void ChangeUIState()
        {
            foreach (var uiObject in objectsToActivate)
            {
                Debug.LogFormat("Activating {0}", uiObject.name);
                uiObject.SetActive(true);
            }

            foreach (var uiObject in objectsToDeactivate)
            {
                Debug.LogFormat("Deactivating {0}", uiObject.name);
                uiObject.SetActive(false);
            }
        }
    }
}