using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    [SerializeField] private Vector3 fixedLocalEulerAngles;
    private void Update()
    {
        transform.eulerAngles = fixedLocalEulerAngles;
    }
}
