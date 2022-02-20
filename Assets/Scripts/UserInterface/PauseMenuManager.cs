using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePauseState();
        }
    }

    public void ChangePauseState()
    {
        Time.timeScale = !isPaused ? 1f : 0f;
        pauseMenu.SetActive(isPaused);
        isPaused = !isPaused;
    }
}
