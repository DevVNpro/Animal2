using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletEnemy : BulletCharacter
{
    private Rigidbody2D _rigidbody2D;
    public int damage;

    protected override void Awake()
    {
        _rigidbody2D = transform.GetComponent<Rigidbody2D>();
 
    }



    protected override void OnEnable()
    {

      if (_bulletSpawner != null)
      {
          _rigidbody2D.velocity = new Vector2(15*(int)_bulletSpawner.characterBullet.moverment.characterDirection,transform.position.y);
      }
      else
      {
          gameObject.SetActive(false);
      }
   


    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            if (!(other.transform.Find("ShieldSoftBlue").gameObject.activeSelf))
            {
                Rxmanager.DeDuctHpPlayer.OnNext(damage);
                other.GetComponent<CharacterHealth>().DeductHealth(damage);
                other.GetComponent<CharacterMoverment>().AddForceTouch(new Vector2(8,0),transform.position);
            }
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.CompareTag("Player"))
        {
            _pool.Release(this);
        }

    }
    


}
