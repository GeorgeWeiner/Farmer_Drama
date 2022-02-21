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
            foreach (var obj in objectsToActivate)
            {
                Debug.LogFormat("Activating {0}", obj.name);
                obj.SetActive(true);
            }

            foreach (var obj in objectsToDeactivate)
            {
                Debug.LogFormat("Deactivating {0}", obj.name);
                obj.SetActive(false);
            }
        }
    }
}