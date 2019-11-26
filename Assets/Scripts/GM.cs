using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GM : MonoBehaviour
{
    public GameObject player;

    UnityEvent playerdeath;
    
    public enum State
    {
        Menu,Playing,GameOver,Loading
    }

    private State current;
    // Start is called before the first frame update
    void Start()
    {
        current = State.Menu;
       
        if (playerdeath == null)
        {
            playerdeath = new UnityEvent();
        }
            playerdeath.AddListener(died);
        

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        PlayerHealth playerHealth = new PlayerHealth();
        var health = playerHealth.health;

        if (health <= 0  && playerdeath != null)
        {
            playerdeath.Invoke();
        }
    }

    void died()
    {
        Debug.Log("player died");
    }


}
