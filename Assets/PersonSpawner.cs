using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersonSpawner : MonoBehaviour
{
    public ColorZone[] zones;
    public TextMeshProUGUI resultGUI;
    public int maxPeople;
    public float interval = 0.5f;
    public List<GameObject> people;

    float peopleCount = 0;

    void Start() {
        InvokeRepeating("SpawnPerson", 0, interval);
    }

    void SpawnPerson() {
        if (peopleCount < maxPeople) {
            Instantiate(people[Random.Range(0, people.Count)], transform.position, Random.rotation);
            peopleCount++;
        }
    }

    public void CheckCorrect() {
        int correct = 0;
        
        foreach (ColorZone colorZone in zones) {
            correct += colorZone.amountCorrect;
        }

        int incorrect = maxPeople - correct;
        if (incorrect > 0) {
            resultGUI.text = "You got " + correct + " correct.";
            resultGUI.text += "\n" + incorrect + " people are misplaced.";
        } else {
            resultGUI.text = "You got all " + maxPeople + " people sorted!";
        }
    }
}
