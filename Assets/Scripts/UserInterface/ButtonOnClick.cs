using System;
using UnityEngine;

namespace UserInterface
{
    public class ButtonOnClick : MonoBehaviour
    {
        private IButton _button;

        public void ExecuteButtonFunctionality(GameObject buttonGameObject)
        {
            _button = buttonGameObject.GetComponent<IButton>();
            _button.ExecuteButtonFunctionality();
        }
    }
}