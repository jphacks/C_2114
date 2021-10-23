using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ImageManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private Text title;

    private int index = 0;


    public void NextImage()
    {
        index++;
        if(index == sprite.Length)
        {
            index = 0;
        }
        ShowImage();
        SetTitle();
    }

    public void BackImage()
    {
        index--;
        if (index < 0)
        {
            index = sprite.Length - 1;
        }
        ShowImage();
        SetTitle();
    }

    public void ShowImage()
    {
        image.sprite = sprite[index];
    }
    public void SetTitle()
    {
        title.text = $"{index+1}.{sprite[index].name}";
    }
}
