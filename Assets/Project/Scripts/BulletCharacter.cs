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
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(20*_characterController.CharaterDirection, 0), ForceMode2D.Impulse);
        }
        if (other.CompareTag("Ground")|| other.CompareTag("Box")|| other.CompareTag("Enemy")|| other.CompareTag("Wall"))
        {
            _pool.Release(this);
        }

    }

    public void SetPool(ObjectPool<BulletCharacter> pool)
    {
        this._pool = pool;
    }
}
