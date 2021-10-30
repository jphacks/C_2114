using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDesign : MonoBehaviour
{
    public void SetGreatDesign(int i)
    {
        PlayerPrefs.SetInt("GreatEffect", i);
    }
    public void SetGoodDesign(int i)
    {
        PlayerPrefs.SetInt("GoodEffect", i);
    }
    public void SetMissDesign(int i)
    {
        PlayerPrefs.SetInt("MissEffect", i);
    }
    public void SetGreatSound(int i)
    {
        PlayerPrefs.SetInt("GreatSound", i);
    }
    public void SetGoodSound(int i)
    {
        PlayerPrefs.SetInt("GoodSound", i);
    }
}
