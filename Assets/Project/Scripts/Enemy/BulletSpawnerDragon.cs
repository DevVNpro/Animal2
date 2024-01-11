using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerDragon : BulletSpawner
{
   
    void Update()
    {
#if  UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            shot();
        }
#endif
    }
}
