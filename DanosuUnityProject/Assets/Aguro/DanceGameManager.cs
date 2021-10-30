using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceGameManager : MonoBehaviour
{
    private bool isPrepared = false;
    [SerializeField] private Dropdown SourceDeviceDropdown;
    [SerializeField] private Button SourceDeviceButton;
    [SerializeField] private Button CloseMenuButton;
    
    // Start is called before the first frame update
    void Start()
    {
    }
    

    // Update is called once per frame
    void Update()
    {
        // Startだとエラーが発生するのでUpdate上で一度だけ動かしています。
        if (!isPrepared)
        {
            SourceDeviceDropdown.value = 1;
            SourceDeviceButton.onClick.Invoke();
            CloseMenuButton.onClick.Invoke();
            isPrepared = true;
        }
        
    }
}
