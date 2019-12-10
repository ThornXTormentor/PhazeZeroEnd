using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoomTrigger : MonoBehaviour
{
    private StartRoomTrigger trigger;
    public GameObject StartDoor;
    private Vector3 doorPos;
    private float doorSpeed = 1.2f;
    private bool open = false;

    private MaterialPropertyBlock propBlock;
    private Renderer Renderer;

    private void Awake()
    {
        trigger = this;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Hand")
        {
            OpenStartDoor();
        }
    }

    private void OpenStartDoor()
    {
        var OffColor = new Color(255, 175, 106, 255);
        var OnColor = new Color(175, 255, 106, 255);
        int Speed = 3;
        int Offset = 2;

        propBlock = new MaterialPropertyBlock();
        Renderer = GetComponent<Renderer>();

        Renderer.GetPropertyBlock(propBlock);
        propBlock.SetColor("_Emission", Color.Lerp(OffColor, OnColor, (Mathf.Sin(Time.time * Speed + Offset) + 1) / 2));

        Renderer.SetPropertyBlock(propBlock);

        doorPos = StartDoor.transform.position;

        open = true;
    }

    private void FixedUpdate()
    {
        if(open == true)
        {
            for(int i = 0; i != -10; i--)
            {
                doorPos.y -= doorSpeed * Time.deltaTime;

                StartDoor.transform.position = doorPos;

                if (StartDoor.transform.position.y < -10)
                {
                    //Destroy(StartDoor);
                }
            }
        }
    }
}
