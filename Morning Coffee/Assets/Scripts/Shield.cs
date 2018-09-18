using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public Animator anim;
    void OnEnable()
    {      
        anim = GetComponent<Animator>();
        anim.SetTrigger("growShield");
    }


}
