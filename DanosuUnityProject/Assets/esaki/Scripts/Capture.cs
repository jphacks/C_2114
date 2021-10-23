using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class Capture : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Text countDown;
    [SerializeField] private Text dateText;
    [SerializeField] private GameObject hideObjects;
    [SerializeField] private GameObject showObjects;

    private string fileFolder = "Assets/Screenshot/";
    private string fileName = null;
    private float waitingTime = 0f;
    private bool shot = false; public void CaptureScreen()
    {
        waitingTime = float.Parse(dropdown.options[dropdown.value].text);
        dateText.text = $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day} {DateTime.Now.ToShortTimeString()}";

        hideObjects.SetActive(false);
        showObjects.SetActive(true);
        shot = true;
    }

    private void Update()
    {
        if (shot)
        {
            waitingTime -= Time.deltaTime;
            countDown.text = Mathf.Ceil(waitingTime).ToString();

            if (waitingTime < 0)
            {
                countDown.text = "";
                fileName = GetFileName();
                ScreenCapture.CaptureScreenshot(GetFilePath());
                Debug.Log("Caputured!");

                StartCoroutine(ResetObjects());
                shot = false;
            }
        }

    }

    private IEnumerator ResetObjects()
    {        
        //なんかスクショ撮影のラグがあるから終わるまで待機
        while (true)
        {
            if (File.Exists(GetFilePath()))
                break; yield return null;
        }
        hideObjects.SetActive(true);
        showObjects.SetActive(false);
    }

    private string GetFileName()
    {
        return $"Shot_{DateTime.Now.Year}{ DateTime.Now.Month}{ DateTime.Now.Day}{ DateTime.Now.Hour}{ DateTime.Now.Minute}{ DateTime.Now.Second}.png";
    }

    public string GetFilePath()
    {
        if (fileName == null)
        {
            return null;
        }
        return fileFolder + fileName;
    }
}
