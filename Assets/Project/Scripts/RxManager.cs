using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class Rxmanager 
{
    #region GamePlay
    public static readonly  Subject<bool> ShakeCameraBrick = new Subject<bool>();
    public static readonly  Subject<bool> CheckPoint = new Subject<bool>();
    public  static  readonly  Subject<bool> UseInfinityBullet = new Subject<bool>();
    public  static  readonly  Subject<bool> UseHealth = new Subject<bool>();
    public  static  readonly  Subject<bool> UseBom= new Subject<bool>();
    public  static  readonly  Subject<bool> UseShield = new Subject<bool>();
    #endregion
    #region UI
    public static readonly Subject<int> DeDuctHpPlayer = new Subject<int>();
    public  static  readonly  Subject<Action> PlayerDie = new Subject<Action>();
    public  static  readonly  Subject<Action> PlayWin = new Subject<Action>();
    public  static  readonly  Subject<Vector3> PickStar = new Subject<Vector3>();

    

    #endregion
}
