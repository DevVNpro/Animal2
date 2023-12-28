using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Platform : MonoBehaviour
    {
        [SerializeField] private bool fixedRotation;
        private Vector3 fixedLocalEulerAngles = new Vector3(0,0,0);
        private Transform child;

        private void Update()
        {
            if (!fixedRotation) return;
            transform.eulerAngles = fixedLocalEulerAngles;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<CharacterMoverment>().isGround && other.gameObject.transform.position.y > transform.position.y)
                {
                other.gameObject.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
                    other.gameObject.transform.SetParent(transform);
                    child = other.gameObject.transform;
                }
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<CharacterMoverment>().isGround && other.gameObject.transform.position.y > transform.position.y)
                {
                    other.gameObject.transform.SetParent(transform);
                    child = other.gameObject.transform;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
            other.gameObject.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Extrapolate;
            other.gameObject.transform.SetParent(null);
                child = null;
            }
        }
    }

