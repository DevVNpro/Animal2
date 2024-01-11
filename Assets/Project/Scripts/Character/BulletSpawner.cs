using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class BulletSpawner : MonoBehaviour
{
    [Header("properties")] 
    [SerializeField] private float timeSpamBullet;
    [SerializeField] private float timeSpamBulletCount;
    public bool isShot;
    [SerializeField] private BulletCharacter _bulletCharacter;
    [SerializeField] private Transform tranformWeapon;
    private ObjectPool<BulletCharacter> _pool;
    public Character characterBullet;
    public void GetReferenceCharacter(Character character)
    {
        characterBullet = character;
    }
 
    private void Awake()
    {
       _pool = new ObjectPool<BulletCharacter>(CreateBullet,OnTakeBulletFromPool,OnReturnBulletFromPool,OnDestoyBullet);
       timeSpamBulletCount = timeSpamBullet;
    }
    private void Update()
    {

        if (isShot)
        {
            timeSpamBulletCount -= Time.deltaTime;
            if (timeSpamBulletCount <= 0)
            {
                isShot = false;
                timeSpamBulletCount = timeSpamBullet;
            }

        }
    }

    public void shot()
    {
        _pool.Get();
        isShot = true;
        
    }

    public BulletCharacter CreateBullet()
    {
        BulletCharacter bulletCharacter = Instantiate(_bulletCharacter, tranformWeapon.position, tranformWeapon.rotation);
        bulletCharacter.SetBulletSpawner(this);
        bulletCharacter.SetPool(_pool);
        return bulletCharacter;
    }

    public void OnTakeBulletFromPool(BulletCharacter bulletCharacter)
    {
        var transform = bulletCharacter.transform;
        transform.position = tranformWeapon.position;
        transform.rotation = tranformWeapon.rotation;
        bulletCharacter.gameObject.SetActive(true);
    }

    public void OnReturnBulletFromPool(BulletCharacter bulletCharacter)
    {
        bulletCharacter.gameObject.SetActive(false);
    }

    public void OnDestoyBullet(BulletCharacter bulletCharacter)
    {
        Destroy(bulletCharacter.gameObject);
    }

    
    
    
}
