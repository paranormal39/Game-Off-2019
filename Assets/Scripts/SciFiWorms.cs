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

    public AudioClip tremor,screech;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject particle;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        //moveto();
        audioSource = GetComponent<AudioSource>();
        isattacking = false;
    }
    private void Awake()
    {
        InvokeRepeating("moveto",0f,12f);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerpos = new Vector3();
        target = GameObject.FindWithTag("Player").transform.position;
        timer -= Time.deltaTime;
        if (timer <= 0.1f && isattacking == false)
        {
            StartCoroutine(attack());
        }
        else if (timer<= 0.1f && isattacking == true){
            timer += 7f;
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

   

}
