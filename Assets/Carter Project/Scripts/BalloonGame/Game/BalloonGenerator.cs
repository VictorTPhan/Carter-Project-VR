using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonGenerator : MonoBehaviour
{
    public enum Mode { grid, rise };

    public Mode spawnMode;

    public int width = 2;
    public int height = 2;
    public float wallWidth = 20;
    public float wallHeight = 20;
    public bool[,] hasBalloon; //2D array

    public bool isSpawning = false;
    public float timePerBalloon = 2f;
    public float minFloatRate = 1;
    public float maxFloatRate = 4;
    public float balloonScale = 1;

    public int numCorrectBalloons = 1;
    public GameObject[] balloons;
    public bool[] correctBalloons;

    private float timer = 0;
    private float widthDiv;
    private float heightDiv;

    // Start is called before the first frame update
    void Start()
    {
        widthDiv = wallWidth / width;
        heightDiv = wallHeight / height;
        hasBalloon = new bool[width, height];
        correctBalloons = new bool[balloons.Length];
        int i = 0;
        while(i < numCorrectBalloons)
        {
            int r = Random.Range(0, balloons.Length);
            if (!correctBalloons[r])
            {
                correctBalloons[r] = true;
                i++;
            }
        }
        Debug.Log("Correct Balloons: ");
        for(int j = 0; j < balloons.Length; j++)
        {
            if (correctBalloons[j]) Debug.Log(balloons[j]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (isSpawning && timer >= timePerBalloon)
        {
            int x = Random.Range(0, width);
            if (spawnMode == Mode.grid)
            {
                int y = Random.Range(0, height);
                if (!hasBalloon[x, y])
                {
                    hasBalloon[x, y] = true;
                    timer = 0;
                    SpawnBalloonGrid(x, y);
                }
            }
            else if (spawnMode == Mode.rise)
            {
                if (!hasBalloon[x, 0])
                {
                    hasBalloon[x, 0] = true;
                    timer = 0;
                    SpawnBalloonRise(x);
                }
            }
        }
    }

    void SpawnBalloonGrid(int x, int y)
    {
        GameObject newBalloon = Instantiate(balloons[RandomBalloon()], transform);
        newBalloon.transform.position = new Vector3(transform.position.x + (x * widthDiv), transform.position.y + (y * heightDiv), transform.position.z);
        Balloon bScript = newBalloon.GetComponent<Balloon>();
        bScript.x = x;
        bScript.y = y;
        bScript.grid = this;
    }

    void SpawnBalloonRise(int x)
    {
        int randomBalloonIndex = RandomBalloon();
        GameObject newBalloon = Instantiate(balloons[randomBalloonIndex], transform);
        newBalloon.transform.position = new Vector3(transform.position.x + (x * widthDiv), transform.position.y, transform.position.z);
        Balloon bScript = newBalloon.GetComponent<Balloon>();
        bScript.x = x;
        bScript.y = 0;
        bScript.minFloatRate = minFloatRate;
        bScript.maxFloatRate = maxFloatRate;
        bScript.balloonScaleModifier = balloonScale;
        bScript.floating = true;
        bScript.grid = this;

        //TODO: Change to a random balloon, not the first one in array
        if(!correctBalloons[randomBalloonIndex])
        {
            bScript.correctBalloon = false;
        }
    }

    int RandomBalloon()
    {
        return Random.Range(0, balloons.Length);
    }
}
