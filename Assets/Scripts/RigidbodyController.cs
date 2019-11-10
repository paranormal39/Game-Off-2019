using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.CoreModule;
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

    public float timerotation;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        airpressure = 1f;
        canJump = true;
        anim = GetComponent<Animator>();
        //Cursor.visble = false;
        //Cursor.lockstate = true;
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
        //movemnt code moved from fixed update
        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0, 0, moveVer) * speed * Time.deltaTime;
        timerotation = 1000f * Time.deltaTime;

        /*
        Vector3 targetdir = movement;
        
        targetdir.y = 0;
        if (targetdir == Vector3.zero)
        {
            targetdir = transform.forward;
            Quaternion tr = Quaternion.LookRotation(targetdir);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, 0f);
            transform.rotation = targetRotation;
        }
        */
        //rigidbody.velocity = movement *speed;
        //rigidbody.AddForce(movement * thrust
        transform.Rotate(0f, moveHor, 0f);
        transform.Translate(movement, Space.Self);

    }

    void FixedUpdate()
    {
        
        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");
        /*
        Vector3 movement = new Vector3(moveHor, 0.0f, moveVer) * speed * Time.deltaTime;
        timerotation = 1000f * Time.deltaTime ;
        Vector3 targetdir = movement;
        targetdir.y = 0;
        if(targetdir == Vector3.zero)
        {
            targetdir = transform.forward;
            Quaternion tr = Quaternion.LookRotation(targetdir);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr,.9f);
            transform.rotation = targetRotation;
        }

        //rigidbody.velocity = movement *speed;
        //rigidbody.AddForce(movement * thrust
        transform.Translate(movement, Space.Self);

    */
        if(moveVer >= 0.1f)
        {
            anim.Play("Walk");
        }

        if (Input.GetKey(KeyCode.Space) && canJump == true)
        {
            jump();
            anim.Play("Jump");
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
