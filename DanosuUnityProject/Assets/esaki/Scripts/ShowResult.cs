using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowResult : MonoBehaviour
{
    [SerializeField] private MusicDataBase musicDataBase;
    [SerializeField] private Text title;
    [SerializeField] private Text difficulty;
    [SerializeField] private Text length;

    [SerializeField] private Text Great;
    [SerializeField] private Text Good;
    [SerializeField] private Text Miss;
    [SerializeField] private Text MaxChain;
    [SerializeField] private Text Score;

    // Start is called before the first frame update
    void Start()
    {
        int index = PlayerPrefs.GetInt("MusicNumber");
        title.text = musicDataBase.GetMusicList()[index].GetTitle();
        difficulty.text = musicDataBase.GetMusicList()[index].GetDifficulty().ToString();
        length.text = musicDataBase.GetMusicList()[index].GetLength();

        // スコアの取得
        Great.text = PlayerPrefs.GetInt("Great").ToString();
        Good.text = PlayerPrefs.GetInt("Good").ToString();
        Miss.text = PlayerPrefs.GetInt("Miss").ToString();
        MaxChain.text = PlayerPrefs.GetInt("MaxChain").ToString();
        Score.text = PlayerPrefs.GetInt("Score").ToString();
    }



}
