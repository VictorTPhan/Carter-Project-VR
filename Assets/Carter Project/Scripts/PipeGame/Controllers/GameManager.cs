using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    WinPipe[] winPipes;
    public bool win;
    public string nextScene;
    public GameObject scoreText;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        winPipes = GameObject.Find("FinalPipe").GetComponentsInChildren<WinPipe>();
        scoreText = GameObject.Find("Score");
        win = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(WinPipe pipe in winPipes)
        {
            score += pipe.waterAmount;
        }
        scoreText.GetComponent<TMP_Text>().text = "Pipe filling: " + score;
        if (winPipes != null && winPipes.All(pipe => pipe.filled))
        {
            win = true;
        }
        if (win)
        {
            GoToNextLevel();
        }
    }

    void GoToNextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }
}
