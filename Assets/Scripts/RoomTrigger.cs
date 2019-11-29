using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public GameObject boss;
    public Transform bossPos;
    private RoomTrigger trigger;

    private void Awake()
    {
        trigger = this;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
             BossSpawner();
        }
    }

    private void BossSpawner()
    {
        Instantiate(boss, bossPos.position, bossPos.rotation);
        Destroy(trigger);
    }
}
