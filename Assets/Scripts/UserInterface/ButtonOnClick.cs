using System;
using UnityEngine;

namespace UserInterface
{
    public class ButtonOnClick : MonoBehaviour
    {
        public void ExecuteButtonFunctionality(GameObject buttonGameObject)
        {
            var _button = buttonGameObject.GetComponent<IButton>();
            _button.ExecuteButtonFunctionality();
        }
    }
}