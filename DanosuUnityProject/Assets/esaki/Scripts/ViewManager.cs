using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRec;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private VViewerScript _vViewer;
    private float t;

    void Awake()
    {
        _vViewer = GetComponent<VViewerScript>();
    }

    public void InitRecordData()
    {
        _vViewer.StartView();
        slider.enabled = false;
    }

    public void StopView()
    {
        _vViewer.StopView();
        t = _vViewer.ViewingTime;
        slider.enabled = true;
    }

    public void ResumeView()
    {
        _vViewer.ResetObjects(t);
        _vViewer.StartView();
    }
    public void PlayRecordData(float t)
    { 
        _vViewer.ResetObjects(t);
        _vViewer.StartView();
        slider.enabled = false;
    }

    public void SetMaxTime(float t)
    {
        slider.maxValue = t;
    }
    public float GetTime()
    {
        return slider.value;
    }

    public void UpdateSlider(float t)
    {
        if (!slider.enabled)
        {
            slider.value = t;
        }
    }
}
