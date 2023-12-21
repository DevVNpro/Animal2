using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class BulletCharacter : MonoBehaviour
{
    [Header("properties")]
    private Rigidbody2D _rigidbody2D;

    private CharacterController _characterController;
    private ObjectPool<BulletCharacter> _pool;
    private void Awake()
    {
        _characterController = FindObjectOfType<CharacterController>();
        _rigidbody2D = transform.GetComponent<Rigidbody2D>();

    }

    private void OnEnable()
    {
        if (_characterController.CharaterDirection == -1)
        {
            transform.localScale= new Vector3(-0.7f,0.7f,0.7f);
        }
        else
        {
            transform.localScale= new Vector3(0.7f,0.7f,0.7f);

        }
        _rigidbody2D.velocity = new Vector2(20*_characterController.CharaterDirection,0);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            if (other.GetComponent<BrickHealth>() != null)
            {    
                other.GetComponent<BrickHealth>().DeductBrickHealth(1);
            }
            Vector3 vector3 = transform.localScale;
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(25*vector3.x,0),ForceMode2D.Impulse);    
        }
        _pool.Release(this);
    }

    public void SetPool(ObjectPool<BulletCharacter> pool)
    {
        this._pool = pool;
    }
}
