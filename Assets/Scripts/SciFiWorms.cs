using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiWorms : MonoBehaviour
{
    public GameObject player;
    public Vector3 target;

    public Animator anim;

    public float timer;
    public bool isattacking;

    public float attkTimer;
   

    public AudioClip tremor,screech,growl,screech2;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject particle;

    [SerializeField] private float health;


    public GameObject targetA, targetB, targetC, targetD,targetE;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        //moveto();
        audioSource = GetComponent<AudioSource>();
        isattacking = false;
        health = 1000f;
        timer = 20f;
        attkTimer = 7;
         }
    private void Awake()
    {
        //InvokeRepeating("moveto",0f,21f);

    }

    // Update is called once per frame
    void Update()
    {
       
        target = GameObject.FindWithTag("Player").transform.position;
        timer -= Time.deltaTime;
        attkTimer -= Time.deltaTime;
        transform.LookAt(target * -1);
        if (timer <= 0.1f && isattacking == false)
        {
            // StartCoroutine(attack()); 
            moveToRandom();
            timer += 25f;
        }
        else if (timer<= 0.1f && isattacking == true){
            timer += 9f;
        }

        if(attkTimer <= 0)
        {
            ranAnim();
            attkTimer += 7;
        }
    }
    IEnumerator attack()
    {
        audioSource.PlayOneShot(screech);
        isattacking = true;
        anim.Play("Take 001");
        yield return new WaitForSeconds(8f);
        //anim.Play("Take 001");
        isattacking = false;
        
    }

    void moveto()
    {
        audioSource.PlayOneShot(tremor);
        Instantiate(particle, target,Quaternion.identity);
        transform.position = new Vector3(target.x, target.y - .5f, target.z);
        anim.Play("Take 001 41");
    }  

    public void TakeDamage()
    {
        health -= 20;
    }

    void moveToRandom()
    {
        int number = Random.RandomRange(0, 3);
        switch (number)
        {
            case 0:
                audioSource.PlayOneShot(tremor);
                Instantiate(particle, targetA.transform.position, Quaternion.identity);
                transform.position = new Vector3(targetA.transform.position.x, targetA.transform.position.y - .5f, targetA.transform.position.z);
                anim.Play("Take 001 41");
                break;
            case 1:
                audioSource.PlayOneShot(tremor);
                Instantiate(particle, targetB.transform.position, Quaternion.identity);
                transform.position = new Vector3(targetB.transform.position.x, targetB.transform.position.y - .5f, targetB.transform.position.z);
                anim.Play("Take 001 41");
                break;
            case 2:
                audioSource.PlayOneShot(tremor);
                Instantiate(particle, targetC.transform.position, Quaternion.identity);
                transform.position = new Vector3(targetC.transform.position.x, targetC.transform.position.y - .5f, targetC.transform.position.z);
                anim.Play("Take 001 41");
                break;
            case 3:
                audioSource.PlayOneShot(tremor);
                Instantiate(particle, targetD.transform.position, Quaternion.identity);
                transform.position = new Vector3(targetD.transform.position.x, targetD.transform.position.y - .5f, targetD.transform.position.z);
                anim.Play("Take 001 41");
                break;
            case 4:
                audioSource.PlayOneShot(tremor);
                Instantiate(particle, targetD.transform.position, Quaternion.identity);
                transform.position = new Vector3(targetE.transform.position.x, targetE.transform.position.y - .5f, targetE.transform.position.z);
                anim.Play("Take 001 41");
                break;
        }
    }
    //here I pass a random animation or attack so its off one timer
   void ranAnim()
    {
        Vector3 playerpos = new Vector3(target.x, target.y, target.z);
        int index = Random.RandomRange(0, 3);
        switch (index)
        {
            case 0:
                audioSource.PlayOneShot(screech);
                anim.Play("roar");
                break;
            case 1:
                transform.LookAt(playerpos);
                anim.Play("attack2");
                audioSource.PlayOneShot(growl);
                break;
            case 2:
                transform.LookAt(target);
                audioSource.PlayOneShot( screech2);
                anim.Play("attack");
                break;

        }
    }

  
}
