using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FireBall : MonoBehaviour
{
    private Tweener rotationTween;

    private void OnEnable()
    {
        float tmp = 0;
        rotationTween= DOVirtual.Float(tmp, 2000, 20f, (tmp) =>
        {
            Debug.Log(tmp);
            transform.rotation = Quaternion.Euler(0f, 0f, tmp);
        });
    }
    private void OnDisable()
    {
        if (rotationTween != null)
        {
            rotationTween.Kill();
        }
    }

}
