using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moverment : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    private Character character;
    public void GetReferenceCharacter(Character character)
    {
        this.character = character;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
