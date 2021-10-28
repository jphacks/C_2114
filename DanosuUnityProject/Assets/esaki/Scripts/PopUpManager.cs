using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject popUp;

    public void ShowPopUp()
    {
        popUp.SetActive(true);
    }

    public void HidePopUp()
    {
        popUp.SetActive(false);
    }
}
