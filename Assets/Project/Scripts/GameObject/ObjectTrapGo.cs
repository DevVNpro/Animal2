using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;
using  UniRx;

public class ObjectTrapGo : MonoBehaviour
{
    

    private void Awake()
    {
        Rxmanager.SwitchTrap.Subscribe((b) => { MoveTrap();}).AddTo(this);
    }

    public void MoveTrap()
    { 
        transform.DOMoveY(5f, 2f);
        Rxmanager.ShakeCameraBrick.OnNext(true);

    }


    


    
    
}
