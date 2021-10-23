using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainDummy : MonoBehaviour
{
    private int musicNumber;
    private const int _maxScore = 99999;

    [SerializeField] private Text musicNumberText;

    void Start()
    {
        // 曲番号の取得
        musicNumber = PlayerPrefs.GetInt("MusicNumber");
        musicNumberText.text = musicNumber.ToString();

    }

    public void LoadResult()
    {
        // スコアの保存
        PlayerPrefs.SetInt("Great", Random.Range(1, 75));
        PlayerPrefs.SetInt("Good", Random.Range(1, 100));
        PlayerPrefs.SetInt("Miss", Random.Range(1, 30));
        PlayerPrefs.SetInt("MaxChain", Random.Range(1, 20));
        PlayerPrefs.SetInt("Score", Random.Range(10, 100) * 100);

        // シーン制御
        PlayerPrefs.SetInt("State", 3);
        SceneManager.LoadScene("Studio3D");
    }
}
