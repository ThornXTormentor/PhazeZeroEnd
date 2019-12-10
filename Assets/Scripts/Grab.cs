using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public GameObject ColObj;
    public GameObject ObjInHand;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            ColObj = other.gameObject;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        ColObj = null;
    }

    public void Update()
    {
        if (Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") > 0.2f && ColObj)
        {
            GrabObj();
        }

        if (Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") < 0.2f && ObjInHand)
        {
            ReleaseObj();
        }

    }

    public void GrabObj()
    {
        ObjInHand = ColObj;
        ObjInHand.transform.SetParent(this.transform);
        ObjInHand.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void ReleaseObj()
    {
        ObjInHand.GetComponent<Rigidbody>().isKinematic = false;
        ObjInHand.transform.SetParent(this.transform);
        ObjInHand = ColObj;
    }
}
