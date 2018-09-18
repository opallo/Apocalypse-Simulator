using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinInfo : MonoBehaviour
{

    public int index;
    public bool selected = false;
    public float rotSpeed = 40;
    public Vector3 originalScale;

    void Awake()
    {
        originalScale = transform.lossyScale;
    }
    void Update()
    {
        
            transform.Rotate(0, rotSpeed, 0);
        
    }

    public void Select()
    {
        if (!selected)
        {
            selected = true;
            transform.localScale += new Vector3(50f, 50f, 50f);
            transform.localPosition += new Vector3(0,0,5f);
        }
    }
    public void Deselect()
    {
        if (selected)
        {
            selected = false;
            transform.localScale -= new Vector3(50f, 50f, 50f);
            transform.localPosition += new Vector3(-5f,0,0);

        }

    }
}
