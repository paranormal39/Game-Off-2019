using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class RigidbodyController : MonoBehaviour
{
    Rigidbody rigidbody;
    public float speed;
    [SerializeField] private float rotatespeed;
    public float thrust;
    public float jumpmag;
    public float airpressure;
    [SerializeField]
    private Slider slider;
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
    public AudioClip footstep, airSfx;
    public AudioSource ac;

    public float setHighPoint;
    public bool passedHighPoint;

    public Slider maxheight;

    public GameObject airfx;
    public float distance;

    public UnityEvent blown;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        airpressure = 1f;
        canJump = true;
        anim = GetComponent<Animator>();
        groundcheckrange = 1f;

        if(blown == null)
        {
            blown = new UnityEvent();
        }
    
    }

    // Update is called once per frame
    void Update()
    {

        
        slider.value = airpressure;
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
         distance = highpoint.y - transform.position.y;
       // maxheight.minValue = transform.position.y;
       // maxheight.maxValue = setHighPoint;
        maxheight.value = distance;
        RaycastHit ground;
        if(transform.position.y > highpoint.y && passedHighPoint ==true)
        {
            //passedHighPoint = true;
            blownaway();
            blown.Invoke();
        }
        else
        {
            passedHighPoint = false;
        }
        if (Physics.Raycast(transform.position, down, out ground, groundcheckrange))
        {
            grounded = true;
            airfx.SetActive(false);
            setHighPoint = 20 + transform.position.y;
            if (Input.GetKey(KeyCode.Space) && grounded == true)
            {
                jump();
                
                
            }

        }
        else {

            grounded = false; 
            if(Input.GetKey(KeyCode.Space) && grounded == false)
            {
                airfx.SetActive(true);
                airRelease();
            }
            if(transform.position.y >highpoint.y)
            {
                passedHighPoint = true;
            }
        }

       
        transform.Rotate(0f, moveHor *speed *Time.deltaTime, 0f);
        transform.Translate(movement, Space.Self);
     }

    void FixedUpdate()
    {

        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");

        float lookHor = Input.GetAxis("Mouse X");
        float lookVer = Input.GetAxis("Mouse Y");

        Vector2 screenlook = new Vector2(0, lookHor) * speed /2;

        transform.Rotate(screenlook);
    
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
        
        Vector3 jumpforce = new Vector3(0, jumpmag, 0) * speed;
        rigidbody.AddForce(jumpforce);
    }

    private IEnumerator refill()
    {
        yield return new WaitForSeconds(2f);
        airpressure += .1f;

    }
    void airRelease()
    {
       
        airpressure -= .009f;
        Vector3 Pressureforce = new Vector3(0, jumpmag, 0) * speed;
        ac.PlayOneShot(airSfx);
        rigidbody.AddForce(Pressureforce);
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
        rigidbody.AddForce(0, 10, 0, ForceMode.Force);
        passedHighPoint = true;

    }

    public void resetrig()
    {
        passedHighPoint = false;
    }
   
}
