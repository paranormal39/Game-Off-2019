﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            health -= 40f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "MethaneLake")
        {
            health -= .5f;
        }
    }

    public void respawn()
    {
        health = 100f;
    }

    public void newrespawn()
    {
        health -= 200f;
    }
}
