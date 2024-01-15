using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterCollector : MonoBehaviour
{
    public TextMeshProUGUI statusGUI;
    bool hasWater = false;

    public void GetWater() {
        hasWater = true;
    }

    public void UpdateGUI() {
        if (hasWater) {
            statusGUI.text = "Success!";
        } else {
            statusGUI.text = "Not quite!";
        }
    }
}
