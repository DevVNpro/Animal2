using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public abstract class BulletCharacter : MonoBehaviour
{   
    [Header("properties")]
    public ObjectPool<BulletCharacter> _pool;

    public BulletSpawner _bulletSpawner;
    protected abstract void Awake();

    protected abstract void OnEnable();

    protected abstract void OnTriggerEnter2D(Collider2D other);

    public void SetPool(ObjectPool<BulletCharacter> pool)
    {
        _pool = pool;
    }
    public  void SetBulletSpawner(BulletSpawner bulletSpawner)
    {
        _bulletSpawner = bulletSpawner;


    }

}
