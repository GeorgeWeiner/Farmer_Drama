using UnityEngine;

namespace UserInterface
{
    public class ButtonQuitGameResponse : MonoBehaviour, IButton
    {
        public void ExecuteButtonFunctionality()
        {
            QuitGame();
        }

        private void QuitGame()
        {
            print("Quitting Application");
            Application.Quit();
        }
    }
}