using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRec;

public class ViewManager : MonoBehaviour
{
    private VViewerScript _vViewer;
    private float t;
    void Awake()
    {
        _vViewer = GetComponent<VViewerScript>();
    }

    public void PlayRecordData()
    {
        _vViewer.StartView();
    }

    public void StopView()
    {
        _vViewer.StopView();
        t = _vViewer.ViewingTime;
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
    }
}
