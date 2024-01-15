using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalloonGameManager : MonoBehaviour
{
    public GameObject scoreText;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.GetComponent<TMP_Text>().text = "Score: " + score;
    }
}
