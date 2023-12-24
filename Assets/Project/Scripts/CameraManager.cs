using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;
using UniRx;

public class CameraManager : MonoBehaviour
{
    [SerializeField]private ProCamera2D _proCamera2D;
    [SerializeField] private GameObject Player;

    private void Awake()
    {

        Rxmanager.PlayerDie.Subscribe((tmp)=>
        {
            LockCamera();
        }).AddTo(this);
        Rxmanager.ShakeCameraBrick.Subscribe((b =>
            {
                ShakeCamera();
            })
            ).AddTo(this);


    }

    public void LockCamera()
    {
        //_proCamera2D.RemoveCameraTarget(Player.transform);
       // _proCamera2D.GetComponent<ProCamera2DCameraWindow>().enabled = false;
       _proCamera2D.enabled = false;
    }

    public void ShakeCamera( )
    {
        _proCamera2D.GetComponent<ProCamera2DShake>().Shake(0);
    }
}
