using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public GameObject player, spawnA,spawnB,spawn0;

    public int spawnIndex;

   


   public UnityEvent playerdeath,respawn;
   // public float health;
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

        if(respawn == null)
        {
            respawn = new UnityEvent();

        }
        respawn.AddListener(respawnPlayer);


        Cursor.visible = false;
        Screen.lockCursor = true;
        

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        

        if (playerHealth.health <= 0  && playerdeath != null)
        {
            current = State.GameOver;
            playerdeath.Invoke();
            Cursor.visible = true;
            Screen.lockCursor = false;
        }
    }

    void died()
    {
        Debug.Log("player died");
    }

    public void respawnPlayer()
    {
        current = State.Playing;
        Cursor.visible = false;
        Screen.lockCursor = true;
        switch (spawnIndex) {
            case 0:
                player.transform.position = spawn0.transform.position;
                break;
            case 1:
                player.transform.position = spawnA.transform.position;
                break;
            case 2:
                player.transform.position = spawnB.transform.position;
                break;


        }

    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    public void initrespawn()
    {
        respawn.Invoke();
    }

    public void spawnadd()
    {
        spawnIndex = 1;
    }

    public void turnoncursor()
    {
        Cursor.visible = true;
        Screen.lockCursor = false;
    }

}
