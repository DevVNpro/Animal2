using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class ObjectGach : MonoBehaviour
{
    public List<GameObject> cracks;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player")&&( transform.position.y  -other.transform.position.y  >3.987f))
        {
            foreach (var crack in cracks)
            {
                crack.SetActive(true);
                Rigidbody2D rigidbody2D = crack.GetComponent<Rigidbody2D>();
                rigidbody2D.gravityScale = 2;
                rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                int randomNumbery = Random.Range(5, 9);
                int randomNumberx = Random.Range(1, 5);
                rigidbody2D.velocity =(new Vector2(randomNumberx,randomNumbery));
                
            }

            transform.GetComponent<TilemapCollider2D>().enabled = false;
            transform.GetComponent<TilemapRenderer>().enabled = false;
            StartCoroutine(Disableobject());

        }
    }

    IEnumerator Disableobject()
    {
        yield return  new WaitForSeconds(3f);
        foreach (var VARIABLE in cracks)
        {
            VARIABLE.SetActive(false);
        }
    }
}
