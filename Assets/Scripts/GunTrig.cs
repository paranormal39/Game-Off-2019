using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTrig : MonoBehaviour
{
    public GameObject camera, gun;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            camera.SendMessage("isequipped");
            gun.SetActive(true);
        }
    }
}
