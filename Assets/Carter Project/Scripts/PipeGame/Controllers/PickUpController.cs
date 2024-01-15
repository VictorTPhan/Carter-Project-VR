using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Rigidbody rb;
    public Collider coll;
    public Transform player, itemContainer, fpsCam;
    public Camera cam;

    public float dropForwardForce = 2;
    public float dropUpwardForce = 2;

    public bool equipped;
    public static bool slotFull;

    private void Awake()
    {
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Transform>();
        fpsCam = playerObject.transform.GetChild(0).GetComponent<Transform>();
        cam = playerObject.transform.GetChild(0).GetComponent<Camera>();
        itemContainer = fpsCam.GetChild(0).GetComponent<Transform>();
    }

    private void Start()
    {
        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }


    void Update()
    {
        //If looking at object and pressed "E"
        if(!equipped && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // Laser pointer
            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.transform.gameObject;
                if (objectHit == this.gameObject)
                {
                    PickUp();
                }
            }
        }

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    public void PickUp()
    {
        equipped = true;
        slotFull = true;

        transform.SetParent(itemContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;

        if (transform.childCount > 0 && transform.GetChild(0).GetComponent<BallGenerator>() != null)
        {
            transform.GetChild(0).GetComponent<BallGenerator>().output = false;
        }
    }

    public void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
    }
}
