using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;

public class RotationAnimation : MonoBehaviour
{
    public float rotationSpeed = 300f;

    private void Start()
    {
        StartCoroutine(RotateContinuously());
    }

    private IEnumerator RotateContinuously()
    {
        while (true)
        {
            transform.Rotate(0f, 0f, rotationSpeed *10* Time.deltaTime);

            yield return null;
        }
    }
}
