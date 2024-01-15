using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDespawn : MonoBehaviour
{
    public float despawnTimer = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteBall());
    }

    IEnumerator DeleteBall()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pipe")
        {
            Destroy(this.gameObject);
        }
    }
}
