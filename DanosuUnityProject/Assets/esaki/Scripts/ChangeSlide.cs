using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSlide : MonoBehaviour
{
    [SerializeField] private Image[] slides;
    private int index = 0;

    public void NextSlide()
    {
        if (index < slides.Length - 1)
        {
            slides[index].enabled = false;
            index++;
            slides[index].enabled = true;
        }
    }
    public void BackSlide()
    {
        if (index > 0)
        {
            slides[index].enabled = false;
            index--;
            slides[index].enabled = true;
        }
    }

    public int GetSlideIndex()
    {
        return index;
    }
}
