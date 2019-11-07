using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RigidbodyController : MonoBehaviour
{
    Rigidbody rigidbody;
    public float speed;
    public float thrust;
    public float jumpmag;
    public float airpressure;
    public Slider slider;
    private float Timer = 3f;
    public bool canJump;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        airpressure = 100f;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = airpressure;
        Timer -= Time.deltaTime;

        if(airpressure <= .01f && canJump ==true )
        {
            StartCoroutine(refill());
            canJump = false;

        }else if( airpressure >= 1f)
        {
            airpressure = 1f;
            StopCoroutine(refill());
            StopCoroutine(adjustrefill());
            canJump = true;
        }

        if(Timer <= .01f)
        {
            StartCoroutine(adjustrefill());
            Timer = 5f;
            canJump = true;
        }
        
        

    }

    void FixedUpdate()
    {
        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHor, 0.0f, moveVer) * speed;

        //rigidbody.velocity = movement;
        rigidbody.AddForce(movement * thrust);

        if (Input.GetKey(KeyCode.Space) && canJump == true)
        {
            jump();
        }
    }
    void jump() {
        airpressure -= .009f;
        Vector3 jumpforce = new Vector3(0, jumpmag, 0) * speed;
        rigidbody.AddForce(jumpforce);
            }

    private IEnumerator refill()
    {
        yield return new WaitForSeconds(2f);
        airpressure+= .1f;
       
    }

    private IEnumerator adjustrefill()
    {
        yield return new WaitForSeconds(2f);
        airpressure += .3f;

    }
}
