using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{

    [ReadOnly(true)] public bool PlayerWin;
    [ReadOnly(true)] public bool Playerlose;

    private void Awake()
    {
     //   Rxmanager.PlayerDie.Subscribe()
    }


    void Start()
    {
        Application.targetFrameRate = 60;
    }


}
