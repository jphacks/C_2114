using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Music", menuName = "MyScriptable/CreateMusic")]
public class Musics : ScriptableObject
{
    public enum Difficulty
    {
        easy,
        normal,
        difficult,
        expert,
        master
    }
    [SerializeField] private string title;
    [SerializeField] private VideoClip videoClip;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private string length;
    [SerializeField] private Difficulty difficulty;
    [SerializeField] private Sprite icon;
    private int highestScore = 0;

    public string GetTitle()
    {
        return title;
    }
    public VideoClip GetVideo()
    {
        return videoClip;
    }
    public AudioClip GetAudio()
    {
        return audioClip;
    }
    public Difficulty GetDifficulty()
    {
        return difficulty;
    }
    public string GetLength()
    {
        return length;
    }

    public int GetHighestScore()
    {
        return highestScore;
    }

    public void UpdateHighestScore(int score)
    {
        highestScore = score;
    }
}


