using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameObject ScoreBoard;
    [SerializeField] private GameObject MusicInfoBoard;
    [SerializeField] private FadeScript FadePanel;

    public Text TitleText;
    public Text DifficultyText;
    public Text TimeText;
    public Text ScoreText;

    public Text Title;
    public Text Difficulty;
    public Text Great;
    public Text Good;
    public Text Miss;
    public Text MaxChain;
    public Text Score;

    public void ShowScore()
    {
        UpdateScoreBoard();
        MusicInfoBoard.SetActive(false);
        ScoreBoard.SetActive(true);
    }
    public void ShowInfo()
    {
        MusicInfoBoard.SetActive(true);
        ScoreBoard.SetActive(false);
    }

    public void ChangeToInfo()
    {
        FadePanel.CrossFade();
        StartCoroutine(_ShowInfo());
    }
    IEnumerator _ShowInfo()
    {
        yield return null;
        while (!FadePanel.Fade)
        {
            yield return null;
        }
        MusicInfoBoard.SetActive(true);
        ScoreBoard.SetActive(false);
    }

    public void UpdateScoreBoard()
    {
        Great.text = PlayerPrefs.GetInt("Great").ToString();
        Good.text = PlayerPrefs.GetInt("Good").ToString();
        Miss.text = PlayerPrefs.GetInt("Miss").ToString();
        MaxChain.text = PlayerPrefs.GetInt("MaxChain").ToString();
        Score.text = PlayerPrefs.GetInt("Score").ToString();
    }
}
