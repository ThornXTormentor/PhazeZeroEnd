using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomTrigger : MonoBehaviour
{
    public GameObject boss;
    public Transform bossPos;

    private int enemyCount;
    public GameObject[] enemies;
    private float xPos;
    private float yPos;
    private float zPos;

    public GameObject spawner;
    private bool spawning = false;
    private bool spawned = false;

    private RoomTrigger trigger;
    private int maxEnemy = 3;
    private int minEnemy = 2;

    private void Awake()
    {
        trigger = this;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player" && trigger.tag == "BossRoom")
        {
             BossSpawner();
        }
        else if(collision.gameObject.tag == "Player" && trigger.tag == "NormalRoom")
        {
            enemyCount = Random.Range(minEnemy, maxEnemy);

            StartCoroutine(EnemySpawner());
            Debug.Log("Called spawner");
        }
    }

    private void BossSpawner()
    {
        Instantiate(boss, bossPos.position, bossPos.rotation);
        Destroy(trigger);
    }

    IEnumerator EnemySpawner()
    {
        for(int i = 0; i < enemyCount; i++)
        {
            GameObject enemy;

            xPos = Random.Range(-3f, 3f);
            yPos = Random.Range(0.5f, 2f);
            zPos = Random.Range(-3f, 3f);
            enemy = enemies[Random.Range(0, enemies.Length)];

            if(enemy.tag == "GroundEnemy")
            {
                Instantiate(enemy, new Vector3(spawner.transform.position.x + xPos, spawner.transform.position.y + 0.2f, spawner.transform.position.z + zPos), Quaternion.identity);
            }
            if (enemy.tag == "FloatingEnemy")
            {
                Instantiate(enemy, new Vector3(spawner.transform.position.x + xPos, spawner.transform.position.y + yPos, spawner.transform.position.z + zPos), Quaternion.identity);
            }
                    
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(trigger);
        Debug.Log("Room Entered and Spawned Enemies");
    }

}
