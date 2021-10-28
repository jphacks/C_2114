using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private BgmManager bgm;
    [SerializeField] private AnimationManager anim;
    [SerializeField] private MusicManager3D musicManager;
    [SerializeField] private SoundManager sound;
    [SerializeField] private BoardManager board;
    [SerializeField] private ViewManager viewer;
    [SerializeField] private FadeScript FadePanel;
    [SerializeField] private AudioSource SE;
    public enum State
    {
        None,
        Title,
        MusicSelection,
        Result
    };
    public State state;

    private void Awake()
    {
        // デバッグ用　インスペクタからステート値を代入
        if (state != State.None)
        {
            PlayerPrefs.SetInt("State", (int)state);
        }
    }

    private void Start()
    {
        switch ((State)PlayerPrefs.GetInt("State"))
        {
            case State.Title: { 
                    bgm.PlayTitleBGM(); 
                    board.ShowInfo(); 
                    break; }
            case State.MusicSelection: {  
                    anim.InRoom(); 
                    board.ShowInfo(); 
                    musicManager.PlayVideo(); 
                    break; }
            case State.Result: {  
                    bgm.PlayResultBGM(); 
                    sound.PlaySound(15);
                    viewer.PlayRecordData();
                    musicManager.SetInfoOnScoreBoard(PlayerPrefs.GetInt("MusicNumber")); 
                    board.ShowScore(); 
                    anim.InResult(); 
                    break; }
            default: { break; }
        }
        // シーンロード後はゲームを終了してもタイトルからスタートできるようにリセット
        PlayerPrefs.SetInt("State", 1);
    }
    public void PlayGame()
    {
        SetMusic();
        FadePanel.FadeOut();
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        yield return null;
        while (!FadePanel.Fade || SE.isPlaying)
        {
            yield return null;
        }
        SceneManager.LoadScene("DanceGame3D");
    }

    private void SetMusic()
    {
        PlayerPrefs.SetInt("MusicNumber", musicManager.GetMusicIndex());
    }
}
