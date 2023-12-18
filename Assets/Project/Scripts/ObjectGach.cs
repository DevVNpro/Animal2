using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using  DG.Tweening;

public class ObjectGach : MonoBehaviour
{
    public List<GameObject> cracks;
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player") && (transform.position.y - other.transform.position.y > 3.3f))
        {
            foreach (var crack in cracks)
            {
                crack.SetActive(true);
                Rigidbody2D rigidbody2D = crack.GetComponent<Rigidbody2D>();
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
}
