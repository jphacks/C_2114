using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private AudioSource SE;

    public void LoadTitleScene()
    {
        StartCoroutine(LoadSceneInSeconds("Title"));
    }

    public void LoadHowToPlayScene()
    {
        StartCoroutine(LoadSceneInSeconds("HowToPlay"));
    }

    public void LoadCreditScene()
    {
        StartCoroutine(LoadSceneInSeconds("Credit"));
    }
    public void LoadMusicSelectionScene()
    {
        StartCoroutine(LoadSceneInSeconds("MusicSelection"));
    }
    public void LoadDanceGameScene()
    {
        StartCoroutine(LoadSceneInSeconds("DanceGame"));
    }
    public void LoadOptionScene()
    {
        StartCoroutine(LoadSceneInSeconds("Option"));
    }
    public void LoadResultScene()
    {
        SceneManager.LoadScene("Result");
    }

    // 効果音が終わるまで待機
    public IEnumerator LoadSceneInSeconds(string sceneName)
    {
        while (SE.isPlaying)
        {
            yield return null;
        }
        
        SceneManager.LoadScene(sceneName);
    }
}
