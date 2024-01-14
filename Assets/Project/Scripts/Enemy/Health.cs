using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHp;
    public int currentHp;
    public Character character;
    public AudioClip soundDead;
    public AudioClip soundDeductHp;

    public void GetReferenceCharacter(Character character)
    {
        this.character = character;
    }

    public virtual void AddCharacterHp(int values)
    {
        currentHp += values;
        if (currentHp > maxHp) currentHp = maxHp;
        if (soundDeductHp != null) character.simpleSound.Play(soundDeductHp);
    }

    public virtual void DeductCharacterHp(int values)
    {
        currentHp -= values;
        Debug.Log("Minus 1 hp enemy");
        if (currentHp <= 0)
        {
            if (soundDead != null) character.simpleSound.Play(soundDead);
            character.Dead();
        }
    }



   
}
