using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DanceGameManager : MonoBehaviour
{
    private bool isCameraPrepared = false;
    [SerializeField] private Dropdown SourceDeviceDropdown;
    [SerializeField] private Button SourceDeviceButton;
    [SerializeField] private Button CloseMenuButton;
    [SerializeField] private GameObject DanceMovieCanvas;
    [SerializeField] private GameObject OsuGameObject;

    private float timer = 0;
    private bool isDanceMovieCanvasEnabled = false;
    [SerializeField] private float enableDanceMovieCanvasTime;

    private bool isOsuGameObjectEnabled = false;
    [SerializeField] private float enableOsuGameObjectTime;

    // Start is called before the first frame update
    void Start()
    {
    }
    

    // Update is called once per frame
    void Update()
    {
        // Startだとエラーが発生するのでUpdate上で一度だけ動かしています。
        if (!isCameraPrepared)
        {
            SourceDeviceDropdown.value = 0;
            SourceDeviceButton.onClick.Invoke();
            CloseMenuButton.onClick.Invoke();
            isCameraPrepared = true;
        }

        timer += Time.deltaTime;
        if (timer >= enableDanceMovieCanvasTime && !isDanceMovieCanvasEnabled)
        {
            DanceMovieCanvas.SetActive(true);
            isDanceMovieCanvasEnabled = true;
        }
        if (timer >= enableOsuGameObjectTime && !isOsuGameObjectEnabled)
        {
            OsuGameObject.SetActive(true);
            isOsuGameObjectEnabled = true;
        }
    }
}
