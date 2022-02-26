using System;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject musicPlayer;
    [SerializeField] AudioSource gameMusic;
    private bool isPaused;

    private void Awake()
    {
        pauseMenu.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePauseState();
        }
    }

    public void ChangePauseState()
    {
        Time.timeScale = isPaused ? 0f : 1f;
        pauseMenu.SetActive(isPaused);
        //musicPlayer.SetActive(!isPaused);
        isPaused = !isPaused;
        if (isPaused)
            gameMusic.Play();
        else
            gameMusic.Stop();
    }
}
