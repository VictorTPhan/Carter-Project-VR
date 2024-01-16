using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;

public class BalloonGenerator : MonoBehaviour
{
    public TextMeshProUGUI instructionGUI;
    public TextMeshProUGUI hitGUI;
    public TextMeshProUGUI missGUI;
    public int hitsToWin;
    public int missesToLose;
    public GameObject[] balloonPrefabs;
    public float interval;
    public Transform[] locations;
    private List<Transform> occupiedLocations = new List<Transform>();
    private List<Transform> unoccupiedLocations = new List<Transform>();

    public float minFloatRate = 1;
    public float maxFloatRate = 4;
    public float minBalloonScale = 0.5f;
    public float maxBalloonScale = 1.5f;
    public float lifeTimeDuration = 5f;
    private bool gameOver = false;
    private int hits = 0;
    private int misses = 0;

    public void Start() {
        foreach (Transform location in locations){ 
            unoccupiedLocations.Add(location);
        }

        instructionGUI.text = "Use the side triggers and pop " + hitsToWin + " balloons to win! If it goes too high, it counts as a miss. " + missesToLose + " misses and you're out!";
    }

    public void Begin() {
        InvokeRepeating("SpawnBalloon", 0, interval);
            hitGUI.text = "Hits: 0";
        missGUI.text = "Misses: 0";
    }

    public bool IsOccupied(Transform location) {
        return occupiedLocations.Contains(location);
    }

    public void MarkOccupied(Transform location) {
        occupiedLocations.Add(location);
        unoccupiedLocations.Remove(location);
    }

    public void MarkUnOccupied(Transform location) {
        unoccupiedLocations.Add(location);
        occupiedLocations.Remove(location);
    }

    public void SpawnBalloon() {
        if (unoccupiedLocations.Count <= 0 || gameOver) {
            return;
        }

        //pick a random unoccupied spot
        Transform randomSpot = unoccupiedLocations[Random.Range(0, unoccupiedLocations.Count)];

        //pick a random balloon
        GameObject randomBalloon = balloonPrefabs[Random.Range(0, balloonPrefabs.Length)];

        //spawn the balloon
        GameObject clone = Instantiate(randomBalloon, randomSpot.position, randomBalloon.transform.rotation);

        //calculate random float rate and size
        float floatRate = Random.Range(minFloatRate, maxFloatRate);
        float scale = Random.Range(minBalloonScale, maxBalloonScale);

        //give the balloon its information
        clone.GetComponent<Balloon>().Initialize(this, randomSpot, floatRate, scale, lifeTimeDuration);

        MarkOccupied(randomSpot);
    }

    public void RecordHit() {
        if (gameOver) return;

        hits++;
        hitGUI.text = "Hits: " + hits.ToString();
        if (hits >= hitsToWin) {
            Win();
        }
    }

    public void RecordMiss() {
        if (gameOver) return;
        
        misses++;
        missGUI.text = "Misses: " + misses.ToString();
        if (misses >= missesToLose) {
            Lose();
        }
    }

    public void Win() {
        gameOver = true;
        hitGUI.text = "You won!";
        missGUI.enabled = false;
    }

    public void Lose() {
        gameOver = true;
        missGUI.text = "You lost!";
        hitGUI.enabled = false;
    }
}
