using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceGameManager : MonoBehaviour
{
    [SerializeField] private GameObject LuneAvater;
    
    // Start is called before the first frame update
    void Start()
    {
        LuneAvater.transform.localScale = Vector3.one*4.0f;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
