using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public enum ESoundType
{
    Enemy,
    PlayerVoice,
    PlayerAttack,
    EnemyHit,
    EnemyDies,
    GameOver,
    ItemDrop,
    ScytheHitSound,
    HealthUp,
    Upgrade,
    CocainIsGoingCrazy,
    OnWaveBegin,
    PlayerWalking,
    TomatoSound,
    TomatoExplosion,
    DoggoAttacks
}


public class SoundManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI voiceLineText;
    [SerializeField] private List<SoundFile> soundEffects;
    [SerializeField] private List<SoundFile> playerAttackSounds;
    [SerializeField] private List<VoiceLine> voiceLines;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
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
    public void PlayRandomVoiceLine()
    {
        int randomVoiceLine = Random.Range(0,voiceLines.Count);
        CreateAudioObject(voiceLines[randomVoiceLine]);
        StartCoroutine(ShowVoiceLineText(voiceLines[randomVoiceLine]));

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
    IEnumerator ShowVoiceLineText(VoiceLine line)
    {
        voiceLineText.gameObject.SetActive(true);
        voiceLineText.text = line.VoicelineText;
        yield return new WaitForSeconds(line.AudioClip.length);
        voiceLineText.gameObject.SetActive(false);
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
    [SerializeField][TextArea(10,10)] private string voicelineText;
    public string VoicelineText => voicelineText;
}