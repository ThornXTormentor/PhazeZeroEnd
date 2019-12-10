using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private Animator liftAnimator;

    private bool lookingAtButton = false;
    private bool liftUp = false;
    private Lift liftButton;

    public GameObject liftPlat;

    private void Awake()
    {
        liftAnimator = liftPlat.GetComponent<Animator>();

        liftButton = this;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Hand" && !liftUp)
        {
            liftAnimator.SetBool("ElevatorUp", true);

            liftUp = true;
        }
        else if (collision.gameObject.tag == "Hand" && liftUp)
        {
            liftAnimator.SetBool("ElevatorUp", false);

            liftUp = false;
        }
    }

    private void OnGUI()
    {
        if (lookingAtButton == true)
        {

        }
    }
}
