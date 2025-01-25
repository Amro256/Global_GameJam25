using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterMovement : MonoBehaviour
{
    //Variables
    private Rigidbody toasterRb; //Reference to the Rigidbody
    private float maxForce = 1000f;
    private float LaunchPower;
    private float powerMultiplier = 10f;
    private Vector3 InitalMousePosition; // Vector 3 used to track the intial mouse position

    private bool isLaunched = false;// Bool to check if the toaster has been launched
    private bool isDragging = false; // Bool to handle if the player is dragging the mouse down or not

    [SerializeField] float launchForce = 20f; // Launch force can be adjusted in the inspector to find a good balance
    [SerializeField] float GravityForce = 9.81f; // Earth's gravity force

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

        if(Input.GetMouseButtonDown(0)) // Left mouse button 
        {
            InitalMousePosition = Input.mousePosition; //Sets the intial mouse position to the current position of the mouse
            //Set is dragging to true
            isDragging = true;
            Debug.Log("Left click is being held down");

        }

        //Check if the mouse is being held down and the player is dragging
        if(isDragging && Input.GetMouseButtonDown(0))
        {
            //Calculate the distance
            Vector3 currentMousePosition = Input.mousePosition;
            float dragDistance = InitalMousePosition.y - currentMousePosition.y; 
            //Launch power
            LaunchPower = Mathf.Clamp(dragDistance * powerMultiplier, 0, maxForce);
        }

        if(Input.GetMouseButtonUp(0)) //When the left mouse button is released
        {
            //Launch the toaster and set dragging to fale
            LaunchToaster();
            Debug.Log("Button released!");
            isDragging = false;
        } 
    }

    //Method to launch the toaster
    private void LaunchToaster() 
    {

        toasterRb.AddForce(Vector3.right * launchForce);
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
}
