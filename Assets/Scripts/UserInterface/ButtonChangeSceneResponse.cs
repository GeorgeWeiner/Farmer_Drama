using UnityEngine;
using UnityEngine.SceneManagement;
using UserInterface;

public class ButtonChangeSceneResponse : MonoBehaviour, IButton
{
    [SerializeField] private string sceneName;
    public void ExecuteButtonFunctionality()
    {
        ChangeScene();
    }
    private void ChangeScene()
    {
        Debug.LogFormat("Switching Scene: {0}", sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
