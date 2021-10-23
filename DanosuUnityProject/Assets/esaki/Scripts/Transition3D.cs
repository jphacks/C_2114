using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition3D : MonoBehaviour
{
    [SerializeField] private GameObject TitleGUI;
    [SerializeField] private GameObject MusicSelectionGUI;
    [SerializeField] private GameObject CreditGUI;
    [SerializeField] private GameObject ResultGUI;

    public void LoadMusicSelection3D()
    {
        MusicSelectionGUI.SetActive(true);
        Debug.Log("MusicSelection loaded");
    }
    public void LoadTitle3D()
    {
        TitleGUI.SetActive(true);
        Debug.Log("Title loaded");
    }

    public void LoadResult3D()
    {
        ResultGUI.SetActive(true);
        Debug.Log("Result loaded");
    }

    public void LoadCredit3D()
    {
        CreditGUI.SetActive(true);
        Debug.Log("Credit loaded");
    }

    public void HideAllGUI()
    {
        TitleGUI.SetActive(false);
        MusicSelectionGUI.SetActive(false);
        CreditGUI.SetActive(false);
        ResultGUI.SetActive(false);
    }
}
