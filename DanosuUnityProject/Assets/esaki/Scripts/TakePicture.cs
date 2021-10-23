using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System;

public class TakePicture : MonoBehaviour
{

    [SerializeField] private RenderTexture RenderTextureRef;
    [SerializeField] private GameObject ScreenImg;
    [SerializeField] private Text countDown;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioClip clip;

    private string folder = "/Screenshot/";
    private float waitingTime;
    private bool shot;

    private void Update()
    {
        if (shot)
        {
            waitingTime -= Time.deltaTime;
            countDown.text = Mathf.Ceil(waitingTime).ToString();

            if (waitingTime < 0)
            {
                countDown.text = "";
                SavePng(GetFileName());
                shot = false;
                soundManager.PlaySound(clip);
                StartCoroutine(_ShotAnim());
            }
        }

    }

    private string GetFileName()
    {
        return $"Shot_{DateTime.Now.Year}{ DateTime.Now.Month}{ DateTime.Now.Day}{ DateTime.Now.Hour}{ DateTime.Now.Minute}{ DateTime.Now.Second}.png";
    }

    private void SavePng(string fileName)
    {
        Texture2D tex = new Texture2D(RenderTextureRef.width, RenderTextureRef.height, TextureFormat.RGB24, false);
        RenderTexture.active = RenderTextureRef;
        tex.ReadPixels(new Rect(0, 0, RenderTextureRef.width, RenderTextureRef.height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        UnityEngine.Object.Destroy(tex);

        //Write to a file in the project folder
        File.WriteAllBytes(Application.dataPath + folder + fileName, bytes);
    }
    private IEnumerator _ShotAnim()
    {
        float a = 0.4f;
        for (int i = 0; 1 - a * i > 0; i++)
        {
            ScreenImg.GetComponent<RawImage>().color = new Color(1, 1, 1, 1 - a * i);
            yield return null;
        }
        for (int i = 1; a * i < 1; i++)
        {
            ScreenImg.GetComponent<RawImage>().color = new Color(1, 1, 1, a*i);
            yield return null;
        }
        ScreenImg.GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
    }

    public void TakeShot()
    {
        waitingTime = 5f;
        shot = true;
    }

}