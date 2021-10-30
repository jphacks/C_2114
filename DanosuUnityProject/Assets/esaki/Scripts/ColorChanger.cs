using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Material mat;
    public enum WallColor
    {
        White,
        Gray,
        Red,
        Pink,
        Blue,
        SkyBlue,
        Green,
        YellowGreen,
        Yellow,
        Black
    }

    private void Start()
    {
        switch ((WallColor)PlayerPrefs.GetInt("WallColor"))
        {
            case WallColor.White: { ColorWhite(); break; }
            case WallColor.Gray: { ColorGray(); break; }
            case WallColor.Red: { ColorRed(); break; }
            case WallColor.Pink: { ColorPink(); break; }
            case WallColor.Blue: { ColorBlue(); break; }
            case WallColor.SkyBlue: { ColorSkyBlue(); break; }
            case WallColor.Green: { ColorGreen(); break; }
            case WallColor.YellowGreen: { ColorYellowGreen(); break; }
            case WallColor.Yellow: { ColorYellow(); break; }
            case WallColor.Black: { ColorBlack(); break; }
            default: { break; }
        }
    }

    public void ChangeColor(float r, float g, float b)
    {
        mat.color = new Color(r, g, b);
    }
    public void ColorWhite()
    {
        mat.color = new Color(1,1,1);
        PlayerPrefs.SetInt("WallColor", 0);
    }
    public void ColorGray()
    {
        mat.color = new Color(0.5f, 0.5f, 0.5f);
        PlayerPrefs.SetInt("WallColor", 1);
    }
    public void ColorRed()
    {
        mat.color = new Color(1, 0, 0);
        PlayerPrefs.SetInt("WallColor", 2);
    }
    public void ColorPink()
    {
        mat.color = new Color(1, 0, 1);
        PlayerPrefs.SetInt("WallColor", 3);
    }
    public void ColorBlue()
    {
        mat.color = new Color(0, 0.5f, 1);
        PlayerPrefs.SetInt("WallColor", 4);
    }
    public void ColorSkyBlue()
    {
        mat.color = new Color(0, 1, 1);
        PlayerPrefs.SetInt("WallColor", 5);
    }
    public void ColorGreen()
    {
        mat.color = new Color(0, 0.5f, 0);
        PlayerPrefs.SetInt("WallColor", 6);
    }
    public void ColorYellowGreen()
    {
        mat.color = new Color(0, 1, 0);
        PlayerPrefs.SetInt("WallColor", 7);
    }
    public void ColorYellow()
    {
        mat.color = new Color(1, 1, 0);
        PlayerPrefs.SetInt("WallColor", 8);
    }
    public void ColorBlack()
    {
        mat.color = new Color(0, 0, 0);
        PlayerPrefs.SetInt("WallColor", 9);
    }
}
