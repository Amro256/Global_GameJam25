using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBounce : MonoBehaviour
{
    ToasterBounceProto toaster;
    public SphereCollider h1;
    SphereCollider toasterCollider;

    [SerializeField] GameObject bubble;

    // Start is called before the first frame update

    private void Start()
    {
        toaster = FindObjectOfType<ToasterBounceProto>();
        //toasterCollider = toaster.GetComponent<SphereCollider>();

    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
    private void BubbleLaunchUp()
    {
        toaster.toasterRb.AddForce(Vector3.right * (toaster.launchForce));
        toaster.toasterRb.AddForce(Vector3.up * (toaster.launchForce - 300f));
        Debug.Log("Toaster has been launched!");


        if (toaster.toasterRb.velocity.x == 0)
        {
            toaster.toasterRb.velocity = Vector3.zero;
            toaster.toasterRb.drag = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BubbleLaunchUp();
            Debug.Log("sigma");
            Destroy(bubble);
        }
    }

    
}
