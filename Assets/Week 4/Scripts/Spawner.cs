using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject SpawnerPrefab;
    public float maxTime;
    public float minTime;
    float timerTarget;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timerTarget = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>timerTarget)
        {
            Instantiate(SpawnerPrefab, transform);
            timer = 0;
            timerTarget = Random.Range(minTime, maxTime);
        }
    }
}
