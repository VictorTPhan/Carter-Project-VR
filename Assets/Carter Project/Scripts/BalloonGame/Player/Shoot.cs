using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Camera cam;
    public BalloonGenerator ballGen;
    public BalloonGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        ballGen = GameObject.Find("BalloonGrid").GetComponent<BalloonGenerator>();
        gameManager = GameObject.Find("GameManager").GetComponent<BalloonGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.transform.gameObject;
                if (objectHit.tag == "Balloon")
                {
                    Balloon balloon = objectHit.GetComponent<Balloon>();
                    ballGen.hasBalloon[balloon.x, balloon.y] = false;
                    Destroy(objectHit);
                    gameManager.score = (balloon.correctBalloon) ? gameManager.score + 1 : gameManager.score - 1;
                }
            }
        }
    }
}
