using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrig : MonoBehaviour
{
   public AudioClip clip;
   public AudioSource ac;
    public GameObject gameObject;
    private int index;
    // Start is called before the first frame update

    void OnStart()
    {
        ac = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && index <= 0)
        {
            ac.PlayOneShot(clip);
            gameObject.SetActive(true);
            index++;
        }
    }
}
