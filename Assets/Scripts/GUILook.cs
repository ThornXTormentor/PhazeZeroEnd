using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILook : MonoBehaviour
{
    public GameObject camera;

    private void OnGUI()
    {
        RaycastHit lookDirectionHit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out lookDirectionHit))
        {
            if(lookDirectionHit.transform.tag == "StartDoorButton")
            {
                GUI.Box(new Rect(250, 250, 300, 50), "Press this with Hand");
                Debug.Log("Looking at button");
            }

            if (lookDirectionHit.transform.tag == "Gun")
            {
                GUI.Box(new Rect(250, 250, 300, 50), "(Grab) R Trigger -> Fire");
                Debug.Log("Looking at gun");
            }

            if (lookDirectionHit.transform.tag == "LiftButton")
            {
                GUI.Box(new Rect(250, 250, 300, 50), "Press this with Hand");
                Debug.Log("Looking at lift");
            }
        }
    }
}
