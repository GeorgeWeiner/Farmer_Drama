using UnityEngine;

namespace UserInterface
{
    public class ButtonPauseStateResponse : MonoBehaviour, IButton
    {
        public void ExecuteButtonFunctionality()
        {
            FindObjectOfType<PauseMenuManager>().ChangePauseState();
        }
    }
}