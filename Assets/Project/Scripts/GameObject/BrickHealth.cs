using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BrickHealth : MonoBehaviour
{
   public int health;
   public List<GameObject> cracks;
    public Vector3 vector3; 
   public void DeductBrickHealth(int damage)
   {
      health -= damage;
      if (health <= 0)
      {
         SoundManager.Intance.PlayBreakBox();
         foreach (var crack in cracks)
         {
            crack.SetActive(true);
            Rigidbody2D rigidbody2D = crack.GetComponent<Rigidbody2D>();
            rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rigidbody2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
            rigidbody2D.interpolation = RigidbodyInterpolation2D.Extrapolate;
            rigidbody2D.gravityScale = 2;
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            int randomNumbery = Random.Range(5, 9);
            int randomNumberx = Random.Range(-3, 4);
            rigidbody2D.velocity = (new Vector2(randomNumberx, randomNumbery));
         }            
         transform.GetComponent<Collider2D>().enabled = false;
         transform.GetComponent<SpriteRenderer>().enabled = false;
         StartCoroutine(Disableobject());
      }
   }
   IEnumerator Disableobject()
   {
      yield return  new WaitForSeconds(1f);
      foreach (var crack in cracks)
      {
         yield return  new WaitForSeconds(0.1f);
         float initfloat = 1;
         DOVirtual.Float(initfloat, 0f, 2f, (values) =>
         {
            crack.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,values);
         });
      }
   }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletPlayer"))
        {
            DeductBrickHealth(1);
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(25 * vector3.x, 0), ForceMode2D.Impulse);
        }
    }

}
