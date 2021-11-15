using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAvatar : MonoBehaviour
{
    public void SetAvatar(int i)
    {
        PlayerPrefs.SetInt("AvatarNumber", i);
    }
}
