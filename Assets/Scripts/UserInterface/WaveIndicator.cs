using TMPro;
using UnityEngine;
using UserInterface;

public class WaveIndicator : MonoBehaviour, IUserInterfaceElement
{
    //Not sure this works, I didn't test this yet.
    
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioSource _audioSource;
    
    private int _waveCount; 
    private SpawnManager _spawnManager;
    private Animator _animator;
    private static readonly int OnWaveBegin = Animator.StringToHash("OnWaveBegin");

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _animator = GetComponent<Animator>();
       
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
        text.text = "Wave" + _waveCount;
        
        OnWaveBeginAnimation();
    }

    private void OnWaveBeginAnimation()
    {
        _animator.SetTrigger(OnWaveBegin);
        
        SoundManager.instance.PlayAudioClip(ESoundType.OnWaveBegin, _audioSource);
    }
}
