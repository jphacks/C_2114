using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDataBase", menuName = "MyScriptable/CreateSoundDataBase")]
public class SoundDataBase : ScriptableObject
{
    [SerializeField] private List<AudioClip> Sounds = new List<AudioClip>();

    public List<AudioClip> GetSounds()
    {
        return Sounds;
    }

}
