using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI GreatText;
    [SerializeField] private TextMeshProUGUI GoodText;
    [SerializeField] private TextMeshProUGUI MissText;
    [SerializeField] private TextMeshProUGUI ChainText;
    [SerializeField] private TextMeshProUGUI MaxChainText;
    [SerializeField] private TextMeshProUGUI ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Great", 0);
        PlayerPrefs.SetInt("Good", 0);
        PlayerPrefs.SetInt("Miss", 0);
        PlayerPrefs.SetInt("Chain", 0);
        PlayerPrefs.SetInt("MaxChain", 0);
        PlayerPrefs.SetInt("Score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        GreatText.text = PlayerPrefs.GetInt("Great").ToString();
        GoodText.text = PlayerPrefs.GetInt("Good").ToString();
        MissText.text = PlayerPrefs.GetInt("Miss").ToString();
        ChainText.text = PlayerPrefs.GetInt("Chain").ToString();
        MaxChainText.text = PlayerPrefs.GetInt("MaxChain").ToString();
        ScoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }
}