using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    //mouse look variables
    Vector2 rotation = new Vector2(0, 0);
    public float cspeed = 3;
   public Camera camera;
    public float pitch;
    public float yaw;
   
    
    
    // Make this customizable later
    public float mouseSensitivity;
    private int yInvert = -1;
    private float yInput, xInput;
    private float currentPitch = 0;
    public float minYaw;
    public float maxYaw;

    public CharacterController characterController;

    public Ray Ray;

    public float jumpspeed;
    public float gravity = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 6f;
        camera = Camera.main;
        jumpspeed = 60f;
        characterController = GetComponent<CharacterController>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 jump = new Vector3(0, 0, 0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Vector3 jump = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            jump.y = jumpspeed;
            // transform.Translate(0, 5, 0, Space.Self);

        }
        jump.y -= gravity * Time.deltaTime;
        characterController.Move(jump * Time.deltaTime);
        
        playermove();
}

   

    private void FixedUpdate()
    {
        //playermove();

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //rb.velocity=  movement * speed;
        //characterController.Move(movement);
        // rb.AddRelativeForce(movement,ForceMode.Force);
        // if (movement != Vector3.zero)
        //  transform.forward = movement;
        //mouse look code 
        //rb.AddForce(movement,ForceMode.VelocityChange);
        //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        //transform.Translate(movement,Space.Self);
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
     // camera.transform.eulerAngles = (Vector2)rotation * cspeed;
         transform.localRotation = Quaternion.Euler(0, rotation.y, 0);
      
       //rigidbody.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 90); 


    }
    void movePlayerMouse()
    {
        yInput = Input.GetAxisRaw("Mouse Y") * pitch * mouseSensitivity * yInvert;
        xInput = Input.GetAxisRaw("Mouse X") * yaw * mouseSensitivity;

        // Player rotates left and right (and camera with)
        transform.Rotate(Vector3.up * xInput);

        // Only camera rotates vertically
        currentPitch = Mathf.Clamp(currentPitch + yInput, minYaw, maxYaw);
        camera.transform.localEulerAngles = Vector3.right * currentPitch; 
        //rb.rotation =  Quaternion.Euler(0, 0, Mathf.Atan2(yInput, xInput) * Mathf.Rad2Deg - 90);
    } 

    public void playermove()
    {
        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHor, 0.0f, moveVer) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
    } 

   


}
