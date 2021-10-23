using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ImageChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] sprite;

    [SerializeField] private GameObject FadePanel;
    private int index = 0;

    private bool isChanging;
    public float speed = 1f;
    float a, r, g, b;

    private void Start()
    {
        r = FadePanel.GetComponent<Image>().color.r;
        g = FadePanel.GetComponent<Image>().color.g;
        b = FadePanel.GetComponent<Image>().color.b;
        isChanging = false;
    }

    public void NextImage()
    {
        index++;
        if (index == sprite.Length)
        {
            index = 0;
        }
        ChangeImage();
    }

    public void BackImage()
    {
        index--;
        if (index < 0)
        {
            index = sprite.Length - 1;
        }
        ChangeImage();
    }

    public void ChangeImage()
    {
        if (isChanging)
        {
            return;
        }
        isChanging = true;
        StartCoroutine(_FadeOutFase());
    }

    IEnumerator _FadeOutFase()
    {
        while (a < 1)
        {
            a += speed;
            FadePanel.GetComponent<Image>().color = new Color(r, g, b, a);
            yield return null;
        }
        _ChangeImage();
    }

    private void _ChangeImage()
    {
        GetComponent<Image>().sprite = sprite[index];
        StartCoroutine(_FadeInFase());
    }

    IEnumerator _FadeInFase()
    {
        while (a > 0)
        {
            a -= speed;
            FadePanel.GetComponent<Image>().color = new Color(r, g, b, a);
            yield return null;
        }
        isChanging = false;
    }
}
