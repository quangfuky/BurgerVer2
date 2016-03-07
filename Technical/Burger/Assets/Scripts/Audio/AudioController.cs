using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using UnityEngine.UI;

public enum TypeAudio
{
    NONE = 0,
    BG_SCREEN_START = 1,
    BG_SCREEN_WORLD_MAP = 2,
    BG_SCREEN_GAME_PLAY = 3,
    BG_SCREEN_GAME_COMPLETE = 4,

    TIENG_PHAO_1 = 5,
    TIENG_PHAO_2 = 6,
    TIENG_PHAO_3 = 7,

    COUNTDOWN = 8,
    SUCCESS = 9,
    FAIL = 10,
    END = 11,
    TICK = 12,

}

[System.Serializable]
public class AudioInfo
{
    public TypeAudio type;
    public AudioClip audioSource;
}
public class AudioController : MonoSingleton<AudioController>
{
    public List<AudioInfo> listAudio;
    public AudioSource audioSourceBg;
    public AudioSource audioSourceEffect;
    public AudioSource audioSourceTick;

    private Slider sliderBg;
    private Slider sliderEffect;
    public TypeAudio type;
    [ContextMenu("Play")]
    void testPlay()
    {
        PlayAudio(type);

    }

    [ContextMenu("Stop")]
    void testStop()
    {
        StopAudio(type);
    }

    void Awake()
    {
    }

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        //sliderBg = GameObject.FindWithTag("Volume").GetComponent<Slider>();
        //sliderEffect = GameObject.FindWithTag("Music").GetComponent<Slider>();
        PlayAudio(TypeAudio.BG_SCREEN_GAME_PLAY);
    }


    // Update is called once per frame
    void Update()
    {
        VolumneAudioBG();
        VolumneAudioEffect();
    }
    public void PlayAudio(TypeAudio type)
    {
        AudioClip source = GetAudio(type);
        audioSourceBg.clip = source;
        audioSourceBg.Play();
    }
    public void PlayAudioEffect(TypeAudio type)
    {
        if (audioSourceEffect.isPlaying == false)
        {
            AudioClip source = GetAudio(type);
            audioSourceEffect.clip = source;
            audioSourceEffect.Play();
        }
    }
    public void PlayAudioTick(TypeAudio type)
    {
        audioSourceTick.Stop();
        AudioClip source = GetAudio(type);
        audioSourceTick.clip = source;
        audioSourceTick.Play();
    }
    public void StopAudioEffect()
    {
        audioSourceEffect.Stop();
        //audioSourceEffect.isPlaying = false;
    }
    public void StopAudio(TypeAudio type)
    {
        AudioClip source = GetAudio(type);
        audioSourceBg.clip = source;
        audioSourceBg.Stop();
    }
    public void VolumneAudioBG()
    {
        //sliderBg = GameObject.FindWithTag("Volume").GetComponent<Slider>();
        if (sliderBg != null)
        {
            audioSourceBg.volume = sliderBg.value;
        }
    }
    public void VolumneAudioEffect()
    {
        //sliderEffect = GameObject.FindWithTag("Music").GetComponent<Slider>();
        if (sliderEffect != null)
        {
            audioSourceEffect.volume = sliderEffect.value;
        }
    }
    public void MuteAudioBG()
    {
        audioSourceBg.mute = !audioSourceBg.mute;
    }
    public void MuteAudioEffect()
    {
        audioSourceEffect.mute = !audioSourceEffect.mute;
    }

    public AudioClip GetAudio(TypeAudio type)
    {
        for (int i = 0; i < listAudio.Count; i++)
        {
            if (listAudio[i].type == type)
            {
                return listAudio[i].audioSource;
            }
        }
        return null;
    }
}
