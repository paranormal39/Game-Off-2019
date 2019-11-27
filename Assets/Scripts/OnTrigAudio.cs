using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigAudio : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audio;
    public MeshRenderer mesh;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();

    }

   void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player" && index <= 0)
        {
            audio.PlayOneShot(audioClip);
            index++;
        }
    }
}
