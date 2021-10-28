using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    // attatch Panel
    public float speed = 1f;
    float a, r, g, b;
    private bool isFading;
    public bool Fade;

    private void Start()
    {
        r = GetComponent<Image>().color.r;
        g = GetComponent<Image>().color.g;
        b = GetComponent<Image>().color.b;
        isFading = false;
        Fade = false;
    }

    public void FadeOut()
    {
        if (isFading)
        {
            StopAllCoroutines();
        }
        isFading = true;
        StartCoroutine(_FadeOut());
    }

    IEnumerator _FadeOut()
    {
        while (a < 1)
        {
            a += speed;
            GetComponent<Image>().color = new Color(r, g, b, a);
            yield return null;
        }
        isFading = false;
        Fade = true;
        Debug.Log("FadeEnd");
    }
    public void FadeIn()
    {
        if (isFading)
        {
            StopAllCoroutines();
        }
        isFading = true;
        Fade = false;
        StartCoroutine(_FadeIn());
    }

    IEnumerator _FadeIn()
    {
        while (a > 0)
        {
            a -= speed;
            GetComponent<Image>().color = new Color(r, g, b, a);
            yield return null;
        }
        isFading = false;
        Debug.Log("FadeEnd");
    }

    public void CrossFade()
    {
        if (isFading)
        {
            StopAllCoroutines();
        }
        isFading = true;
        StartCoroutine(_CrossFade());
    }
    IEnumerator _CrossFade()
    {
        while (a < 1)
        {
            a += speed;
            GetComponent<Image>().color = new Color(r, g, b, a);
            yield return null;
        }
        Fade = true;
        while (a > 0)
        {
            a -= speed;
            GetComponent<Image>().color = new Color(r, g, b, a);
            yield return null;
        }
        isFading = false;
        Fade = false;
        Debug.Log("FadeEnd");
    }
}
