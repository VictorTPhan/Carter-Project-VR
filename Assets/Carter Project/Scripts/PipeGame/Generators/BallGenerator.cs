using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public GameObject ball;
    public Transform[] origin; //Origin points of water (where they come out of)
    public float cooldown = 0.5f;
    public bool output;
    public bool pipe;

    private float waitForWaterDespawn = 1f;

    public GameObject[] outputs; //Disables output colliders

    private float time = 0f;
    private float despawn = 0f;

    // Update is called once per frame
    void Update()
    {
        if(pipe)
        {
            time += Time.deltaTime;
            if(time > cooldown)
            {
                output = false;
                despawn += Time.deltaTime;
                if(despawn > waitForWaterDespawn)
                {
                    foreach (GameObject o in outputs)
                    {
                        o.GetComponent<BoxCollider>().enabled = true;
                    }
                }
            }
        }
        if (output)
        {
            foreach(GameObject o in outputs)
            {
                o.GetComponent<BoxCollider>().enabled = false;
            }
            foreach(Transform t in origin)
            {
                GameObject water = Instantiate(ball, t);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
        {
            output = true;
            time = 0f;
            despawn = 0f;
        }
    }
}
