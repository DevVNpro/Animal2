using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FireBall : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
        transform.position  = new Vector2( player.transform.position.x, player.transform.position.y+1.3f);
    }
    private Tweener rotationTween;

    private void OnEnable()
    {
        transform.SetParent(null);
        float tmp = 0;
        rotationTween= DOVirtual.Float(tmp, 2000, 20f, (tmp) =>
        {
            Debug.Log(tmp);
            transform.rotation = Quaternion.Euler(0f, 0f, tmp);
        });
    }
    private void OnDisable()
    {
        transform.SetParent(player.transform);
        if (rotationTween != null)
        {
            rotationTween.Kill();
        }
    }

}
