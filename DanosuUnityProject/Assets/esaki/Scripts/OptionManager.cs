using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public void SetSoundIndex(int i)
    {
        PlayerPrefs.SetInt("SoundIndex", i);
    }
}
