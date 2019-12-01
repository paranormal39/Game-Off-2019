using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class aimState : MonoBehaviour
{
    public float range = 150f;
    public float damage = 10f;

    public GameObject fpscam;

    public CinemachineComposer composer;

    public float sensitvityY;

    [SerializeField]
    private AudioClip blaster;

  
    private AudioSource audio;

    public GameObject enemy;

    public bool equipped;

    public void Start()
    {
        composer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
        audio = GetComponent<AudioSource>();
        //enemy = GameObject.FindWithTag("Enemy");
        equipped = false;
    }


    // Update is called once per frame
    void Update()
    {
        //enemy = GameObject.FindWithTag("Enemy");
        float vertx = Input.GetAxis("Mouse Y") * sensitvityY;
        float horz = Input.GetAxis("Mouse X");

        Vector3 look = new Vector3(vertx, horz, 0)*Time.deltaTime;

        transform.Rotate(look);
        composer.m_TrackedObjectOffset.y += vertx;
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -2.5f, 2.5f);
        Debug.DrawRay(fpscam.transform.position, fpscam.transform.forward, Color.red, range);
        if (Input.GetButtonDown("Fire1") && equipped == true) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            audio.PlayOneShot(blaster);

            enemy.SendMessage("TakeDamage", 25f);
            


        }
    }

    public void isequipped()
    {
        equipped = true;
    }
}
