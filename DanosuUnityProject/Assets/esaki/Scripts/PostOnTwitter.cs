using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class PostOnTwitter : MonoBehaviour
{
    private int score;

    public void Tweet()
    {
        score = PlayerPrefs.GetInt("Score");
        OpenTwitter();
    }

    /*public void TweetScreenshot()
    {
        string filePath = capture.GetFilePath();
        string imagePass = Application.persistentDataPath + filePath;

        if (filePath != null)
        {
            //投稿
            string tweetText = $"DanOsu! でスコア{score}獲得！";
            string tweetURL = "アプリのURL";
            SocialConnector.PostMessage(SocialConnector.ServiceType.Twitter, tweetText, tweetURL, imagePass);
            Debug.Log("ツイート成功");
        }
        else
        {
            Debug.LogWarning("screenshot has not been taken!");
        }
    }*/

    private void OpenTwitter()
    {
        string text = UnityWebRequest.EscapeURL($"DanOsu! でスコア{score}を獲得！\n");
        string tag = UnityWebRequest.EscapeURL("DanOsu");
        string url = $"https://twitter.com/intent/tweet?text={text}&hashtags={tag}";

        Application.OpenURL(url);
    }
}