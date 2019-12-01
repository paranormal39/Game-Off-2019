using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testenemy : MonoBehaviour
{
   public float health = 150;

    // Update is called once per frame
    void Update()
    {
        
    } 

   public void takedmg(float dmg)
    {
        health -= dmg;
    }
}
