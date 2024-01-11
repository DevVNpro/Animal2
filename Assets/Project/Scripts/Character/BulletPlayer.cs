using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class BulletPlayer : BulletCharacter
{
    [Header("properties")]
    private Rigidbody2D _rigidbody2D;
    private CharacterController _characterController;
    protected override void Awake()
    {
        _characterController = FindObjectOfType<CharacterController>();
        _rigidbody2D = transform.GetComponent<Rigidbody2D>();

    }

    protected override void OnEnable()
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

    protected override void OnTriggerEnter2D(Collider2D other)
    {
   
        if (other.CompareTag("Box"))
        {
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(20*_characterController.CharaterDirection, 0), ForceMode2D.Impulse);
        }
        if (other.CompareTag("Ground")|| other.CompareTag("Box")|| other.CompareTag("Enemy")|| other.CompareTag("Wall"))
        {
            _pool.Release(this);
        }
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().DeductCharacterHp(1);
        }
   }


}
