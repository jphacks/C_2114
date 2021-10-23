using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private MusicDataBase musicDataBase;
    [SerializeField] private VideoPlayer videoPlayer;

    [SerializeField] private Text[] title = new Text[3];
    [SerializeField] private Text[] difficulty = new Text[3];
    [SerializeField] private Text[] length = new Text[3];

    private int index = 0;

    public void NextMusic()
    {
        index++;

        if (index == musicDataBase.GetMusicList().Count)
        {
            index = 0;
        }
        SetMusicInfo();
        PlayVideo();
    }
    public void BackMusic()
    {
        index--;
        if (index < 0)
        {
            index = musicDataBase.GetMusicList().Count - 1;
        }
        SetMusicInfo();
        PlayVideo();
    }

    public int GetIndex()
    {
        return index;
    }

    private void SetMusicInfo()
    {
        if (index == 0)
        {
            SetMusicInfo(0, musicDataBase.GetMusicList().Count - 1);
            SetMusicInfo(1, 0);
            SetMusicInfo(2, 1);
        }
        else if (index == musicDataBase.GetMusicList().Count - 1)
        {
            SetMusicInfo(0, index - 1);
            SetMusicInfo(1, index);
            SetMusicInfo(2, 0);
        }
        else
        {
            SetMusicInfo(0, index - 1);
            SetMusicInfo(1, index);
            SetMusicInfo(2, index + 1);
        }

    }

    private void SetMusicInfo(int i, int j)
    {
        title[i].text = musicDataBase.GetMusicList()[j].GetTitle();
        difficulty[i].text = musicDataBase.GetMusicList()[j].GetDifficulty().ToString();
        length[i].text = musicDataBase.GetMusicList()[j].GetLength();
    }

    private void ClearMusicInfo(int i)
    {
        title[i].text = "";
        difficulty[i].text = "";
        length[i].text = "";
    }

    private void PlayVideo()
    {
        videoPlayer.clip = musicDataBase.GetMusicList()[index].GetVideo();
        if (videoPlayer.clip != null)
        {
            videoPlayer.Play();
        }
    }

    public void SetMusicNumber()
    {
        PlayerPrefs.SetInt("MusicNumber", index);
        PlayerPrefs.Save();
    }
}
