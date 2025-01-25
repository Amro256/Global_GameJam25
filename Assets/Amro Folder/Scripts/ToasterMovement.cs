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
    private float HoldDownStartTime; // Private variable that will track how long the left mouse button is being held down
    private bool isLaunched = false;// Bool to check if the toaster has been launched

    [SerializeField] float minLaunchForce = 100f;
    [SerializeField] float maxLaunchForce = 5000f;
    [SerializeField] float maxHoldDownTime = 3f;

    //[SerializeField] float launchForce = 20f; // Launch force can be adjusted in the inspector to find a good balance
    [SerializeField] float GravityForce = 9.81f; // Earth's gravity force


    [Header("Drag values")]
    [SerializeField] float dragIncreaseValue = 1.0f;
    [SerializeField] float maxDrag = 3.0f;


    void Start()
    {
        //Find the Rigidbody of the toaser object
        toasterRb = GetComponent<Rigidbody>();
        toasterRb.drag = 0; // Set's the drag to 0
    }

    // Update is called once per frame
    void Update()
    {
        MouseInput(); //Handles the mouse inputs
    }

    void FixedUpdate() 
    {
        if(isLaunched) //Checks if the toaster has been launched and if so, call the Slow Down method and gravity 
        {
            ToasterSlowDown();
            ToasterGravity();
        }       
    }

    void MouseInput() //This method will be used to handle the mouse dragging 
    {
        if(isLaunched) return; // This will prevent multiple launching by skipping over the code if is launched = true

        if(Input.GetMouseButtonDown(0)) // Left mouse button 
        {
            HoldDownStartTime = Time.time; //Time in seconds since the start of the game (on how long the button has been held down)
            Debug.Log("Left click is being held down");
        }

        if(Input.GetMouseButtonUp(0)) //When the left mouse button is released
        {
            float holdDownTime = Time.time - HoldDownStartTime;
            //Launch the toaster and set dragging to fale
            LaunchToaster(holdDownTime);
            Debug.Log("Button released");
        } 
    }


    //Method to launch the toaster & calculate
    private void LaunchToaster(float holdTime) 
    {    
        float holdDownFactor = Mathf.Clamp01(holdTime / maxHoldDownTime);
        Debug.Log($"Hold Factor: {holdDownFactor}");
        float launchForce = Mathf.Lerp(minLaunchForce, maxLaunchForce, holdDownFactor);

        toasterRb.AddForce(Vector3.right * launchForce);

        Debug.Log("Toaster has been launched! At a launch force of" + launchForce);

        isLaunched = true;

        // if(toasterRb.velocity.x == 0)
        // {
        //     toasterRb.velocity = Vector3.zero;
        //     toasterRb.drag = 0;
        // }
    }

    //----------------------------------------------------Other-Methods------------------------------------------------------------------//


    //Method to slow down the toaster over time using a drag variable
    private void ToasterSlowDown() 
    {
        if(toasterRb.drag < maxDrag) // Checks if the Toaster's drag is less than the max drag
        {
            toasterRb.drag += dragIncreaseValue * Time.deltaTime; //If it is then increase the Toaster's drag by the rate
        }
    }

    //Method to simulate gravity by using a vector 3 down
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
            Debug.Log("Game over!");
        }
    }

    //On trigger method here for the "Bath Tub" and enable the Win Screen! 
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Bath"))
        {
            //Enable the Win Screen Panel
            GameManager.insance.showWinScreen();
            Debug.Log("You lose");
        }    
    }
}
