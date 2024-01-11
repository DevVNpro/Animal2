using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHp;
    public int currentHp;
    public Character character;

    public void GetReferenceCharacter(Character character)
    {
        this.character = character;
    }

    public virtual void AddCharacterHp(int values)
    {
        currentHp += values;
        if (currentHp > maxHp) currentHp = maxHp;
    }

    public virtual void DeductCharacterHp(int values)
    {
        currentHp -= values;
        Debug.Log("Minus 1 hp enemy");
        if(currentHp <=0 ) character.Dead();
    }



   
}
