using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterMovement : MonoBehaviour
{
    //Variables
    private Rigidbody toasterRb; //Reference to the Rigidbody
    private float maxForce = 500f;
    private float LaunchPower;
    private float powerMultiplier = 10f;
    private float HoldDownStart; // Private variable that will track how long the left mouse button is being held down
    private float maxHoldDown = 3f;
    private bool isLaunched = false;// Bool to check if the toaster has been launched

    [SerializeField] float launchForce = 20f; // Launch force can be adjusted in the inspector to find a good balance
    [SerializeField] float GravityForce = 9.81f; // Earth's gravity force
    [SerializeField] float maxButtonHoldDown = 3f;

    [Header("Drag values")]
    [SerializeField] float dragIncreaseValue = 1.0f;
    [SerializeField] float maxDrag = 3.0f;


    void Start()
    {
        //Find the Rigidbody of the toaser object
        toasterRb = GetComponent<Rigidbody>();
        toasterRb.drag = 0; //Intially the drag will be set to 0
    }

    // Update is called once per frame
    void Update()
    {
        MouseInput(); //Handles the mosue inputs
    }

    void FixedUpdate() 
    {
        if(isLaunched) //Checks if the toaster has been launched and if so, call the Slow Down method and gravity 
        {
            ToasterSlowDown();
            ToasterGravity();
        }       
    }

    void MouseInput()
    {
        //This method will be used to handle the mouse dragging 

        if(Input.GetMouseButton(0)) // Left mouse button 
        {
            HoldDownStart = Time.time; //Time in seconds since the start of the game (on how long the button has been held down)
            Debug.Log("Left click is being held down");

        }

        //Check if the mouse is being held down and the player is dragging
        // if( Input.GetMouseButtonDown(0))
        // {
        //     //Calculate the distance
        //     Vector3 currentMousePosition = Input.mousePosition;
        //     float dragDistance = InitalMousePosition.y - currentMousePosition.y; 
        //     //Launch power
        //     LaunchPower = Mathf.Clamp(dragDistance * powerMultiplier, 0, maxForce);
        // }

        if(Input.GetMouseButtonUp(0)) //When the left mouse button is released
        {
            float holdDownTime = Time.time - HoldDownStart;
            //Launch the toaster and set dragging to fale
            LaunchToaster();
            Debug.Log("Button released");
        } 
    }


    //Method to launch the toaster & calculate
    private void LaunchToaster() 
    {    
        // LaunchPower = Mathf.Clamp01(holdTime / maxButtonHoldDown);
        toasterRb.AddForce(Vector3.right * launchForce);
        // toasterRb.AddForce(LaunchPower * maxForce);

        Debug.Log("Toaster has been launched! At a launch force of");

        isLaunched = true;

        if(toasterRb.velocity.x == 0)
        {
            toasterRb.velocity = Vector3.zero;
            toasterRb.drag = 0;
        }
    }


    //Method to slow down the toaster over time 
    private void ToasterSlowDown() //Method to handle the drag of the toaster over time 
    {
        if(toasterRb.drag < maxDrag) // Checks if the Toaster's drag is less than the max drag
        {
            toasterRb.drag += dragIncreaseValue * Time.deltaTime; //If it is then increase the Toaster's drag by the rate
        }
    }

    //Method to simulate gravity
    private void ToasterGravity()
    {
        toasterRb.AddForce(Vector3.down * GravityForce * Time.deltaTime);
    }

    //Check if the player collides with the ground, if so activate the game over panel from the game manager
    
     void OnCollisionEnter(Collision collision) 
    {   
        if(collision.gameObject.CompareTag("Ground"))
        {
            //Call the game over screen here
            GameManager.insance.showGameOverScreen();
        }
    }

    //On trigger method here for the bathtub and enable the win screen panel 
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Bath"))
        {
            //Enable the Win Screen Panel
            GameManager.insance.showWinScreen();
            Debug.Log("Win screen enabled");
        }    
    }
}
