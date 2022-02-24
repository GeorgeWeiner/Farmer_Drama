using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESoundType
{
    Enemy,
    PlayerVoice,
    PlayerAttack,
    EnemyHit,
    EnemyDies,
    GameOver,
    ItemDrop,
    HealthUp,
    Upgrade,
    CocainIsGoingCrazy,
    OnWaveBegin,
    PlayerWalking,
    TomatoSound,
    TomatoExplosion
}


public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<SoundFile> soundEffects;
    [SerializeField] private List<SoundFile> playerAttackSounds;
    [SerializeField] private List<VoiceLine> voiceLines;

    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null && instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlayAudioClip(ESoundType soundType, AudioSource audioSource,bool isBonusSound)
    {
        if (!isBonusSound)
        {
            for (int i = 0; i < soundEffects.Count; i++)
            {
                if (soundEffects[i].SoundType == soundType && ISSoundPlayable(soundEffects[i], soundEffects[i].Offset))
                {
                    audioSource.volume = soundEffects[i].Volume;
                    audioSource.PlayOneShot(soundEffects[i].AudioClip);
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < soundEffects.Count; i++)
            {
                if (soundEffects[i].SoundType == soundType && ISSoundPlayable(soundEffects[i], soundEffects[i].Offset))
                {
                    CreateAudioObject(soundEffects[i]);
                    return;
                }
            }      
        }
       
    }
    public void PlayRandomAttackSound()
    {
        int randomSound = Random.Range(0, playerAttackSounds.Count);
        CreateAudioObject(playerAttackSounds[randomSound]);
    }

    private bool ISSoundPlayable(SoundFile sound, float offset)
    {
        if(sound.SoundTimer - offset <= Time.time)
        {
            SetTimer(sound);
            return true;
        }
        else if (sound.IsStackable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetTimer(SoundFile sound)
    {
        sound.SoundTimer = Time.time + sound.AudioClip.length;
    }

    private void CreateAudioObject(SoundFile fileToPlay)
    {
        var tempObj = new GameObject();
        var tempAudioSource = tempObj.AddComponent<AudioSource>();
        tempAudioSource.volume = fileToPlay.Volume;
        tempAudioSource.PlayOneShot(fileToPlay.AudioClip);
        Destroy(tempObj, fileToPlay.AudioClip.length);
    }
}
[System.Serializable]
public class SoundFile
{
    [SerializeField] private ESoundType soundType;
    public ESoundType SoundType => soundType;

    [SerializeField] private AudioClip audioClip;
    public AudioClip AudioClip => audioClip;

    [SerializeField][Range(0,1)] private float volume;
    public float Volume => volume;

    [SerializeField] private float offSet;
    public float Offset => offSet;

    [SerializeField] private bool isStackable;
    public bool IsStackable => isStackable;

    private float soundTimer;
    public float SoundTimer { get { return soundTimer; } set { soundTimer = value; } }
}

[System.Serializable]
public class VoiceLine : SoundFile
{
    [SerializeField] private string voicelineText;
    public string VoicelineText => voicelineText;
}