using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{

    [ReadOnly(true)] public bool PlayerWin;
    [ReadOnly(true)] public bool Playerlose;

    
    void Start()
    {
        Application.targetFrameRate = 60;
    }


}
