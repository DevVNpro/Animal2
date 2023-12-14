using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;

public class ObjectTrapGo : MonoBehaviour
{
    public bool active;
    private Vector3 _transform;

    private void Start()
    {
        active = true;
        _transform = transform.position;
    }

    void Update()
    {
        if (active)
        {
            StartCoroutine(StartAnim());
        }
    }
    IEnumerator StartAnim()
    {
        
        active = false;
        transform.DOMoveY(-2.5f, 2f);
        yield return  new WaitForSeconds(2f);
        transform.DOMoveY(_transform.y, 3f);
        yield return  new WaitForSeconds(3f);
        Debug.Log("BBBBBBBBBBBBBBBBBB");
        active = true;
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.transform.CompareTag("Player"))
        {
            StopCoroutine(StartAnim());
            StartCoroutine(ResetAnim());

        }

    }
    IEnumerator ResetAnim()
    {
        transform.DOMoveY(_transform.y, 3f);
        yield return  new WaitForSeconds(3f);
        active = true;
    }
    
    
}
