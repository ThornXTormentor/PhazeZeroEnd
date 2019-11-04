using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomModule : MonoBehaviour
{
    public string[] RoomTags;

    public RoomConnector[] GetExitsForRoom()
    {
        return GetComponentsInChildren<RoomConnector>();
    }
}
