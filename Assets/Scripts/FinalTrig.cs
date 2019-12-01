using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalTrig : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy,Player;

    public AudioClip finalsfx;
    public AudioSource audio;

    public UnityEvent spawnindex;

    public int index;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Player = GameObject.FindWithTag("Player"); 

        if(spawnindex == null)
        {
            spawnindex = new UnityEvent();
        }
        
    }

 
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && index == 0)
        {
            spawnindex.Invoke();
            audio.PlayOneShot(finalsfx);
            enemy.SetActive(true);
            index++;
            // Player.SendMessage("finalAnim");
        }
    }
}
