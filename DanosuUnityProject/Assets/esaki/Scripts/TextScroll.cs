using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroll : MonoBehaviour
{
    public float Speed;
    public float MaxY;
    private bool enable;
    private float x, y, z;
    private float _y;

    private IEnumerator routine;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    public void StartScroll()
    {
        enable = true;
        _y = y;
        routine = _Scroll();
        StartCoroutine(routine);
    }
    // Update is called once per frame
    public void StopScroll()
    {
        if (enable)
        {
            StopCoroutine(routine);
            enable = false;
            transform.position = new Vector3(x, y, z);
        }
    }

    private IEnumerator _Scroll()
    {
        while (_y < MaxY)
        {
            Debug.Log($"{_y} < {MaxY}");
            _y += Speed;
            transform.position = new Vector3(x, _y, z);
            yield return null;
        }
        Debug.Log("Credit End");
        enable = false;
        transform.position = new Vector3(x, y, z);
    }
}
