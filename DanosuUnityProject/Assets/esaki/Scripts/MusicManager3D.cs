using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MusicManager3D : MonoBehaviour
{
    [SerializeField] private MusicDataBase musicDataBase;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private BoardManager board;

    [SerializeField] private GameObject content;
    [SerializeField] private GameObject ButtonPrefab;


    private List<Musics> musics;
    private int index;

    private void Awake()
    {
        // データベースから曲を取得
        musics = musicDataBase.GetMusicList();
        if (musics.Count < 1)
        {
            Debug.LogError("データベースに曲が存在しません");
        }        
        // 選択曲の初期設定
        index = 0;
    }
    void Start()
    {
        UpdateInfoBoard();

        // 曲ごとに選択ボタンをスクロールビューに追加
        for (int i = 0; i < musics.Count; i++)
        {
            GameObject btn = Instantiate(ButtonPrefab);
            btn.name = $"{ButtonPrefab.name}{i}";
            btn.transform.SetParent(content.transform, false);

            // サムネイルとしてテキストに難易度・タイトルを表示
            btn.GetComponentInChildren<Text>().text = $"{GetDifficultyText(musics[i].GetDifficulty())}\n{musics[i].GetTitle()}";

            // どのボタンが押されたか判定させる
            var _ = i;
            btn.GetComponent<Button>().onClick.AddListener(() => SelectMusic(_));
        }
    }

    public void SelectMusic(int i)
    {
        if (i < 0 || i > musics.Count - 1)
        {
            return;
        }
        index = i;
        UpdateInfoBoard();
        PlayVideo();
    }

    public int GetMusicIndex()
    {
        return index;
    }
    public void PlayVideo()
    {
        videoPlayer.clip = musics[index].GetVideo();
        if (videoPlayer.clip != null)
        {
            videoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        videoPlayer.Pause();
    }

    public void ResumeVideo(float t)
    {
        if (videoPlayer.clip != null)
        {
            videoPlayer.time = t;
            videoPlayer.Play();
        }
    }

    public void UpdateInfoBoard()
    {
        board.TitleText.text = musics[index].GetTitle();
        board.DifficultyText.text = GetDifficultyText(musics[index].GetDifficulty());
        board.TimeText.text = musics[index].GetLength();
        board.ScoreText.text = musics[index].GetHighestScore().ToString();
    }

    public void SetInfoOnScoreBoard(int i)
    {
        board.Title.text = musics[i].GetTitle();
        board.Difficulty.text = GetDifficultyText(musics[i].GetDifficulty());
    }

    private string GetDifficultyText(Musics.Difficulty difficulty)
    {
        string _difficulty;
        switch (difficulty)
        {
            case Musics.Difficulty.easy: { _difficulty = "★☆☆☆☆"; break; }
            case Musics.Difficulty.normal: { _difficulty = "★★☆☆☆"; break; }
            case Musics.Difficulty.difficult: { _difficulty = "★★★☆☆"; break; }
            case Musics.Difficulty.expert: { _difficulty = "★★★★☆"; break; }
            case Musics.Difficulty.master: { _difficulty = "★★★★★"; break; }
            default: { _difficulty = "☆☆☆☆☆"; break; }
        }
        return _difficulty;
    }

    public float GetCurrentMusicLength()
    {
        return float.Parse(musics[index].GetLength());
    }

    public float GetVideoCurrentTime()
    {
        return (float)videoPlayer.time;
    }
}
