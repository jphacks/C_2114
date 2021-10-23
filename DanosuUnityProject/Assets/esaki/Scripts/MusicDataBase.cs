using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicDataBase", menuName = "MyScriptable/CreateMusicDataBase")]
public class MusicDataBase : ScriptableObject
{
    [SerializeField] private List<Musics> musicList = new List<Musics>();

    public List<Musics> GetMusicList()
    {
        return musicList;
    }
}
