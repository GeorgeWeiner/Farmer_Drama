using UnityEngine;
using UnityEngine.UI;
using UserInterface;

public class ChangeSliderValue : MonoBehaviour, IUserInterfaceElement
{
    [SerializeField] private Slider slider;
    [SerializeField] private Stats stats;

    private void Awake()
    {
        InitializeUIElement();
    }
    
    private void Update()
    {
        UpdateUIElement();
    }
    
    public void InitializeUIElement()
    {
        slider.maxValue = stats.Health;
        slider.value = stats.Health;
    }

    public void UpdateUIElement()
    {
        slider.value = stats.Health;
    }
}
