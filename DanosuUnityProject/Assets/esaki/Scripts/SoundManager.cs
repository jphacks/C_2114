using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SoundDataBase soundDataBase;
    private List<AudioClip> sounds;

    public enum KindOfSound
    {
        Transition,
        Ok,
        Back,
        Play
    }

    private int transitionIndex;
    private int backIndex;
    private int selectIndex;
    private int playIndex;

    private KindOfSound kindOfSound;
    private int selectedIndex;

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void Start()
    {
        sounds = soundDataBase.GetSounds();

        transitionIndex = PlayerPrefs.GetInt("TransitionSoundIndex");
        backIndex = PlayerPrefs.GetInt("BackSoundIndex");
        selectIndex = PlayerPrefs.GetInt("SelectSoundIndex");
        playIndex = PlayerPrefs.GetInt("PlaySoundIndex");

        kindOfSound = KindOfSound.Transition;
        selectedIndex = -1;
    }

    public void PlayTransitionSound()
    {
        audioSource.clip = sounds[transitionIndex];
        audioSource.Play();
    }
    public void PlayBackSound()
    {
        audioSource.clip = sounds[backIndex];
        audioSource.Play();
    }
    public void PlaySelectSound()
    {
        audioSource.clip = sounds[selectIndex];
        audioSource.Play();
    }
    public void PlayPlaySound()
    {
        audioSource.clip = sounds[playIndex];
        audioSource.Play();
    }

    public void PlaySound(int i)
    {
        audioSource.clip = sounds[i];
        audioSource.Play();
    }

    public void ReLoadIndex()
    {
        transitionIndex = PlayerPrefs.GetInt("TransitionSoundIndex");
        backIndex = PlayerPrefs.GetInt("BackSoundIndex");
        selectIndex = PlayerPrefs.GetInt("SelectSoundIndex");
        playIndex = PlayerPrefs.GetInt("PlaySoundIndex");
    }
    public void ApplySound()
    {
        if (selectedIndex == -1) { return; }

        switch (kindOfSound)
        {
            case KindOfSound.Transition: { PlayerPrefs.SetInt("TransitionSoundIndex", selectedIndex); break; }
            case KindOfSound.Ok: { PlayerPrefs.SetInt("SelectSoundIndex", selectedIndex); break; }
            case KindOfSound.Back: { PlayerPrefs.SetInt("BackSoundIndex", selectedIndex); break; }
            case KindOfSound.Play: { PlayerPrefs.SetInt("PlaySoundIndex", selectedIndex); break; }
            default: { break; }
        }
        Debug.Log($"SE Changed : {kindOfSound} {selectedIndex}");
        ReLoadIndex();
    }
    public void ChangeKindOfSound(int i)
    {
        kindOfSound = (KindOfSound)i;
    }

    public void ChangeIndex(int i)
    {
        selectedIndex = i;
    }
}
