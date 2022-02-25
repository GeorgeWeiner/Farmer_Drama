using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : Death
{
    [SerializeField] GameObject gameOverFadeIn;
    [SerializeField] float timeTillGameOverScreen;
    [SerializeField] string gameOverSceneName;
    public override void OnDie()
    {
        SaveSystem.SaveHighScore(SpawnManager.instance.WaveCount);
        GetComponent<Animator>().SetTrigger("isDying");
        gameOverFadeIn.gameObject.SetActive(true);
        StartCoroutine(GameOverScreen());
    }

    IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(timeTillGameOverScreen);
        SceneManager.LoadScene(gameOverSceneName);
    }


}
