using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private Character character;
     public void GetReferenceCharacter(Character character)
    {
        this.character = character;
    }

     public void DeductHpPlayer(GameObject player,int value)
     {
         player.GetComponent<CharacterHealth>().DeductHealth(value);
     }
}
