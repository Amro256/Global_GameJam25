using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBounceRight : MonoBehaviour
{
    ToasterMovement toaster;
    public SphereCollider h3;
    SphereCollider toasterCollider;
    [SerializeField] float bounceForce;

    [SerializeField] GameObject bubble;

    // Start is called before the first frame update

    private void Start()
    {
        toaster = FindObjectOfType<ToasterMovement>();
        //toasterCollider = toaster.GetComponent<SphereCollider>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void BubbleLaunchUp()
    {
        toaster.toasterRb.AddForce(Vector3.right * bounceForce);
        Debug.Log("Toaster has been launched!");

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




