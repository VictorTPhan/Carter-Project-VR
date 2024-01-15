using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class ColorZone : MonoBehaviour
{
    public PersonColor color;
    public int amountCorrect;

    void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Person>(out Person person)) {
            if (person.color == this.color) {
                amountCorrect++;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.TryGetComponent<Person>(out Person person)) {
            if (person.color == this.color) {
                amountCorrect--;
            }
        }
    }
}
