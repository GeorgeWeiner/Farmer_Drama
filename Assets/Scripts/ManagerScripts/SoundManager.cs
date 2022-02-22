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
    OnWaveBegin
}


public class SoundManager : MonoBehaviour
{
    [SerializeField] List<SoundFile> soundEffects;

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

    public void PlayAudioClip(ESoundType soundType, AudioSource audioSource)
    {
        for(int i = 0; i < soundEffects.Count; i++)
        {
            if(soundEffects[i].SoundType == soundType && ISSoundPlayable(soundEffects[i], soundEffects[i].Offset))
            {
                audioSource.volume = soundEffects[i].Volume;
                audioSource.PlayOneShot(soundEffects[i].AudioClip);
                return;
            }
        }
    }

    bool ISSoundPlayable(SoundFile sound, float offset)
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

    void SetTimer(SoundFile sound)
    {
        sound.SoundTimer = Time.time + sound.AudioClip.length;
    }
}

[System.Serializable]
public class SoundFile
{
    [SerializeField] ESoundType soundType;
    public ESoundType SoundType => soundType;

    [SerializeField] AudioClip audioClip;
    public AudioClip AudioClip => audioClip;

    [SerializeField][Range(0,1)] float volume;
    public float Volume => volume;

    [SerializeField] float offSet;
    public float Offset => offSet;

    [SerializeField] bool isStackable;
    public bool IsStackable => isStackable;

    private float soundTimer;
    public float SoundTimer { get { return soundTimer; } set { soundTimer = value; } }
}