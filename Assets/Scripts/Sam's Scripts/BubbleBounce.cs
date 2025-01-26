using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBounce : MonoBehaviour
{
    ToasterMovement toaster;
    public SphereCollider h1;
    SphereCollider toasterCollider;
    [SerializeField] float bounceForce;

    [SerializeField] GameObject bubble;

    public AudioClip[] bubbleSound;

    // Start is called before the first frame update

    private void Start()
    {
        toaster = FindObjectOfType<ToasterMovement>();
        //toasterCollider = toaster.GetComponent<SphereCollider>();
        GetComponent<AudioSource>().playOnAwake = false;
        //GetComponent<AudioSource>().clip = bubbleSound;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void BubbleLaunchUp()
    {
        toaster.toasterRb.AddForce(Vector3.right * bounceForce);
        toaster.toasterRb.AddForce(Vector3.up * bounceForce);
        Debug.Log("Toaster has been launched!");

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BubbleLaunchUp();
            Debug.Log("sigma");
            Destroy(bubble);

            //audio
            int randomIndex = Random.Range(0, bubbleSound.Length);
            GetComponent<AudioSource>().clip = bubbleSound[randomIndex];
            GetComponent<AudioSource>().Play();
        }
    }
}
