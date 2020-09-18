using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public GameObject model;

    private float floatSpeed = 0.3f;
    public float amp = 2;
    private float tempPos;

    void Start()
    {
        model = this.gameObject;

        tempPos = transform.position.y;
    }

    void Update()
    {
        floatModel();
    }

    void floatModel()
    {
        Vector3 yPos = transform.position;

        yPos.y = tempPos + amp * Mathf.Sin(floatSpeed * Time.time);
        transform.position = yPos;
    }
}
