using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp3D : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }
    public void ShowPopUp()
    {
        animator.SetBool("Pop", true);
    }

    public void HidePopUp()
    {
        animator.SetBool("Pop", false);
    }
}
