using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelectManager3D : MonoBehaviour
{
    [SerializeField] private MusicDataBase musicDataBase;
    [SerializeField] private List<Text> thumbnails;

    private void Start()
    {
        SetDefaultMusic();
    }

    private void SetDefaultMusic()
    {
        List<Musics> musics = musicDataBase.GetMusicList();
        int length = musics.Count;
        if (length > thumbnails.Count) { length = thumbnails.Count; }

        for (int i = 0; i < length; i++)
        {
            switch (musics[i].GetDifficulty())
            {
                case Musics.Difficulty.easy : { thumbnails[i].text = "★☆☆☆☆"; break; }
                case Musics.Difficulty.normal : { thumbnails[i].text = "★★☆☆☆"; break; }
                case Musics.Difficulty.difficult : { thumbnails[i].text = "★★★☆☆"; break; }
                case Musics.Difficulty.expert : { thumbnails[i].text = "★★★★☆"; break; }
                case Musics.Difficulty.master : { thumbnails[i].text = "★★★★★"; break; }
                default: { thumbnails[i].text = "☆☆☆☆☆"; break; }
            }
            thumbnails[i].text += "\n" + musics[i].GetTitle();
        }
    }
}
