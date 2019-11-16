using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimState : MonoBehaviour
{
    public float range = 20f;
    public float damage = 10f;

    public GameObject fpscam;


    public void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        float vertx = Input.GetAxis("Mouse X");
        float horz = Input.GetAxis("Mouse Y");

        Vector3 look = new Vector3(vertx, horz, 0)*Time.deltaTime;

        transform.Rotate(look);

        Debug.DrawRay(fpscam.transform.position, fpscam.transform.forward, Color.red, range);
        if (Input.GetButtonDown("Fire1"))
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

        }
    }
}
