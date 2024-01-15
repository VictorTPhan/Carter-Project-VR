using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPipe : MonoBehaviour
{
    public int winThreshold = 1000;
    public int waterAmount = 0;
    public bool filled = false;

    // Update is called once per frame
    void Update()
    {
        if(waterAmount >= winThreshold)
        {
            filled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
        {
            waterAmount++;
        }
    }
}
