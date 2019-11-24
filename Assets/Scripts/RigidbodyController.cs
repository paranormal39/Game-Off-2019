using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.CoreModule;
public class RigidbodyController : MonoBehaviour
{
    Rigidbody rigidbody;
    public float speed;
    [SerializeField] private float rotatespeed;
    public float thrust;
    public float jumpmag;
    public float airpressure;
    //public Slider slider;
    private float Timer = 3f;
    public bool canJump;

    public float timerotation;

    private Animator anim;
    public bool isAiming;

    public GameObject turnoff;

    public bool grounded;
    public float groundcheckrange;

    public float velocity;
    public float motion;
    public float horzmotion;

    public AudioClip[] folley;
    public AudioClip footstep;
    public AudioSource ac;

    public int setHighPoint;
    public bool passedHighPoint;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        airpressure = 1f;
        canJump = true;
        anim = GetComponent<Animator>();
        groundcheckrange = 1f;
        //Cursor.visble = false;
        //Cursor.lockstate = true;
    }

    // Update is called once per frame
    void Update()
    {
       // slider.value = airpressure;
        Timer -= Time.deltaTime;
        velocity = rigidbody.velocity.y;
        motion = Input.GetAxis("Vertical");
        horzmotion = Input.GetAxis("Horizontal");
        anim.SetFloat("motion", motion);
        anim.SetFloat("velocity", velocity);
        anim.SetFloat("horzmotion", horzmotion);
        if (airpressure <= .01f && canJump == true)
        {
            StartCoroutine(refill());
            canJump = false;

        } else if (airpressure >= 1f)
        {
            airpressure = 1f;
            StopCoroutine(refill());
            StopCoroutine(adjustrefill());
            canJump = true;
        } 

        

        if (Timer <= .01f)
        {
            StartCoroutine(adjustrefill());
            Timer = 5f;
            canJump = true;
        }
        //movemnt code moved from fixed update
        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0, 0, moveVer) * speed * Time.deltaTime;


        Vector3 down = new Vector3(0, -1);
        Vector3 highpoint = new Vector3(0, setHighPoint, 0);
        RaycastHit ground;
        if(transform.position.y > highpoint.y)
        {
            passedHighPoint = true;
            blownaway();
        }
        else
        {
            passedHighPoint = false;
        }
        if (Physics.Raycast(transform.position, down, out ground, groundcheckrange))
        {
            grounded = true;
            if (Input.GetKey(KeyCode.Space) && grounded == true)
            {
                jump();
                
            }

        }
        else {

            grounded = false; 

            if(transform.position.y >highpoint.y)
            {
                passedHighPoint = true;
            }
        }

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
        //transform.Rotate(0f, moveHor, 0f);
        transform.Translate(movement, Space.Self);

    }

    void FixedUpdate()
    {

        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");

        float lookHor = Input.GetAxis("Mouse X");
        float lookVer = Input.GetAxis("Mouse Y");

        Vector2 screenlook = new Vector2(0, lookHor) * speed;

        

        

        transform.Rotate(screenlook);
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
        if (moveVer >= 0.1f)
        {
            // play audio foot steps 
            if (!ac.isPlaying)
            {
                StartCoroutine(foleysfx());
            }
            else
            {
                //ac.Stop();
               StopCoroutine(foleysfx());
            }



        }

       

        if (Input.GetButtonDown("Fire2"))
        {
            shoot();
            anim.Play("shoot2");

        }

        if (Input.GetButtonUp("Fire2"))
        {
            anim.SetBool("isAim", false);

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
        airpressure += .1f;

    }

    private IEnumerator adjustrefill()
    {
        yield return new WaitForSeconds(2f);
        airpressure += .3f;

    }

    private void shoot()
    {
        anim.SetBool("isAim", true);
    }

    private IEnumerator foleysfx()
    {
        
        int index = Random.Range(0, folley.Length);
        footstep = folley[index];
        ac.clip = footstep;
        if(!ac.isPlaying)ac.Play();
        yield return new WaitForSeconds(1.5f);

        
    }

    public void blownaway()
    {
        rigidbody.AddForce(0, 200, 0, ForceMode.Impulse);
    }
   
}
