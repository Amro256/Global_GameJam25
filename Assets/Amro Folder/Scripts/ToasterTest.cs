using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterTest : MonoBehaviour
{

    [SerializeField] float Toasterforce = 20f;
    [SerializeField] Rigidbody toasterRb;

    // Start is called before the first frame update
    void Start()
    {
        //Find the Rigidbody of the toaser object
        toasterRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            toasterRb.AddForce(Vector3.right * Toasterforce, ForceMode.Impulse);
        }
    }
}
