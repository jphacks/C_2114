using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GameController;

public class AnimationManager: MonoBehaviour
{
    [SerializeField] private Transition3D transition3D;
    [SerializeField] private Animator DoorController;
    [SerializeField] private Animator CameraController;
    [SerializeField] private Animator SmartphoneController;

    void Start()
    {
        
    }
    public void TestTransition()
    {
        DoorController.SetBool("Open", !DoorController.GetBool("Open"));
        CameraController.SetBool("isInside", !CameraController.GetBool("isInside"));
    }

    public void EnterRoom()
    {
        DoorController.SetBool("Open", true);
        CameraController.SetBool("isInside", true);        
    }
    public void GoOut()
    {
        DoorController.SetBool("Open", false);
        CameraController.SetBool("isInside", false);
    }

    public void InRoom()
    {
        transition3D.HideAllGUI();
        EnterRoom();
        // Skip Animation
        DoorController.Play("OpenDoor", 0, 1);
        CameraController.Play("EnterRoom", 0, 1);
    }
    public void InResult()
    {
        transition3D.HideAllGUI();

        DoorController.SetBool("Open", true);
        CameraController.SetBool("isResult", true);

        // Skip Animation
        DoorController.Play("OpenDoor", 0, 1);
        CameraController.Play("GoResult", 0, 1);
    }

    public void BackFromResult()
    {
        CameraController.SetBool("isResult", false);
        CameraController.SetBool("isInside", true);
    }

    public void PickSmartphone()
    {
        SmartphoneController.SetBool("Pick", true);
    }
    public void LeaveSmartphone()
    {
        SmartphoneController.SetBool("Pick", false);
    }
}
