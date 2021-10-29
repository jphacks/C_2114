using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioClip TitleBGM;
    [SerializeField] private AudioClip ResultBGM;

    private float volume;

    void Start()
    {
        ReLoadVolume();
    }

    public void PlayTitleBGM()
    {
        bgm.clip = TitleBGM;
        bgm.Play();
    }
    public void PlayResultBGM()
    {
        bgm.clip = ResultBGM;
        bgm.Play();
    }

    public void StopBGM()
    {
        StartCoroutine(_FadeOut());
    }
    
    private IEnumerator _FadeOut()
    {
        float d = 0.01f;
        while (bgm.volume > 0)
        {
            bgm.volume -= d;
            yield return null;
        }
        bgm.Stop();
        bgm.volume = volume;
    }

    public void ReLoadVolume()
    {
        volume = PlayerPrefs.GetFloat("Volume");
        bgm.volume = volume;
    }

    public void ChangeVolume(float _volume)
    {
        bgm.volume = _volume;
    }

    public void ApplyVolume()
    {
        PlayerPrefs.SetFloat("Volume", bgm.volume);
    }

    public void MuteBGM()
    {
        bgm.mute = true;
    }
    public void UnMuteBGM()
    {
        bgm.mute = false;
    }
}
