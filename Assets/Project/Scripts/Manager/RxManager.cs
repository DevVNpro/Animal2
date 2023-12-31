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
    public  static  readonly  Subject<int> UseInfinityBullet = new Subject<int>();
    public  static  readonly  Subject<bool> UseHealth = new Subject<bool>();
    public  static  readonly  Subject<int> UseBom= new Subject<int>();
    public  static  readonly  Subject<int> UseShield = new Subject<int>();
    public  static  readonly  Subject<bool> SwitchTrap = new Subject<bool>();
    public  static readonly  Subject<bool> LockCharater = new Subject<bool>();
    public  static readonly  Subject<bool> FreeCharater = new Subject<bool>();
    public  static  readonly  Subject<bool> CollectCoin = new Subject<bool>();
    public  static  readonly  Subject<bool> CollectStar = new Subject<bool>();
    #endregion
    #region UI
    public static readonly Subject<int> DeDuctHpPlayer = new Subject<int>();
    public  static  readonly  Subject<bool> PlayerDie = new Subject<bool>();
    public  static  readonly  Subject<bool> PlayWin = new Subject<bool>();
    public  static  readonly  Subject<Vector3> PickStar = new Subject<Vector3>();

    

    #endregion
}
