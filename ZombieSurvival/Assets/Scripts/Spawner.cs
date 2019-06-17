using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public EnemyController spawningMob;
    public Transform player;
    public float timer = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0f)
        {
            EnemyController mob = Instantiate(spawningMob, this.transform);
            mob.target = player.transform;
            //Debug.Log("Mob Spawned");
            timer = 15f;
        }

        timer -= Time.deltaTime;
    }
}