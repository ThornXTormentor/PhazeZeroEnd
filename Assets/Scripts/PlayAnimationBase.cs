using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationBase : MonoBehaviour
{
    public GameObject model;
    public string animationName;

    void Start()
    {
        model = this.gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            model.GetComponent<Animator>().Play(animationName);
        }
    }
}
