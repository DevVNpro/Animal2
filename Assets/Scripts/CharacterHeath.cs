using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHeath : MonoBehaviour
{
    [Header("properties")]
    [SerializeField] private int heath;
    [SerializeField] private int maxHeath;
    [Header("Ref")]
    [SerializeField] private CharacterController characterController;
    private void Dead()
    {
        characterController._characterMoverment.isDead = true;
        

    }


    public void DeductHeath()
    {
        if(heath == 1)
        {
            heath -= 1;
            Dead();
            return;
        }
        heath -= 1;

    }
    public void AddHeath()
    {
        if (heath == maxHeath) return;
        heath += 1;
    }
}
