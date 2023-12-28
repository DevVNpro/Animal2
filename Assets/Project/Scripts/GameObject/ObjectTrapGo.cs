using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;

public class ObjectTrapGo : MonoBehaviour
{

    private void Start()
    {
        TrapControl.OnActive += MoveTrap;
    }

    public void MoveTrap()
    { 
        transform.DOMoveY(5f, 2f);

    }


    


    
    
}
