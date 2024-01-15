using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public static int score = 0;
    public Collider coll;
    public bool floating;
    public float floatRate = 1;
    public float minFloatRate = 1;
    public float maxFloatRate = 4;
    public float despawnTimer = 5f;

    public bool correctBalloon = true;

    public float balloonScale;
    public float balloonScaleModifier;

    public int x;
    public int y;

    public BalloonGenerator grid;

    private void Start()
    {
        floatRate = Random.Range(minFloatRate, maxFloatRate);

        balloonScale = transform.localScale.x;
        balloonScale = balloonScale * balloonScaleModifier;
        transform.localScale = new Vector3(balloonScale, balloonScale, balloonScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (floating)
        {
            transform.Translate(Vector3.up * Time.deltaTime * floatRate, Space.World);
            StartCoroutine(DestroyBalloon());
        }
    }

    IEnumerator DestroyBalloon()
    {
        yield return new WaitForSeconds(despawnTimer);
        grid.hasBalloon[x, y] = false;
        Destroy(this.gameObject);
    }
}
