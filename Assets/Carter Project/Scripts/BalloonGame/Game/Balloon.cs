using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private float floatRate;
    private BalloonGenerator spawner;
    private Transform spawnLocation;

    public void Initialize(BalloonGenerator spawner, Transform spawnLocation, float floatRate, float scale, float lifeTimeDuration) {
        this.spawner = spawner;
        this.spawnLocation = spawnLocation;
        this.floatRate = floatRate;

        transform.localScale = transform.localScale * scale;
        StartCoroutine(DestroyBalloon(lifeTimeDuration));
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * floatRate, Space.World);
    }

    IEnumerator DestroyBalloon(float lifeTimeDuration)
    {
        yield return new WaitForSeconds(lifeTimeDuration);
        if (gameObject.activeSelf || enabled) {
            Destroy(this.gameObject);
            spawner.MarkUnOccupied(spawnLocation);
            spawner.RecordMiss();
        }
    }

    public void PopBalloon() {
        Destroy(this.gameObject);
        spawner.MarkUnOccupied(spawnLocation);
        spawner.RecordHit();
    }
}
