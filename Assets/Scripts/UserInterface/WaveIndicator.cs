using TMPro;
using UnityEngine;
using UserInterface;

public class WaveIndicator : MonoBehaviour, IUserInterfaceElement
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animation _animation;
    
    public static int _waveCount;
    private SpawnManager _spawnManager;

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        InitializeUIElement();
    }

    public void InitializeUIElement()
    {
        if (_spawnManager != null) 
            _spawnManager.OnWaveBegin += UpdateUIElement;
    }

    public void UpdateUIElement()
    {
        _waveCount++;
        text.text = "Wave " + _waveCount;
        
        OnWaveBeginAnimation();
    }

    private void OnWaveBeginAnimation()
    {
        _animation.Play();
        
        SoundManager.instance.PlayAudioClip(ESoundType.OnWaveBegin, _audioSource,false);
    }
}
