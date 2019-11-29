using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGen : MonoBehaviour
{
    public RoomModule[] Rooms;
    public RoomModule StartRoom;
    public RoomModule BossRoom;
    public RoomModule DeadEnd;
    public int NumOfRooms;

    public int Generations;

    private void Start()
    {
        var start = (RoomModule)Instantiate(StartRoom, transform.position, transform.rotation);
        var pendExits = new List<RoomConnector>(start.GetExitsForRoom());
        bool BossRoomSpawned = false;

        for (int gens = 0; gens <= Generations; gens++)
        {
            var newExit = new List<RoomConnector>();
            string newTag;
            RoomModule newRoomPrefab;
            RoomModule newRoom;
            RoomConnector[] newRoomExits;
            RoomConnector exitMatch;

            foreach (var pendExit in pendExits)
            {
                if (gens == Generations && pendExits.Count > 0 && BossRoomSpawned == false)
                {
                    newRoom = (RoomModule)Instantiate(BossRoom);
                    newRoomExits = newRoom.GetExitsForRoom();
                    exitMatch = newRoomExits.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newRoomExits);
                    MatchExit(pendExit, exitMatch);
                    BossRoomSpawned = true;
                }
                else if (gens == Generations && pendExits.Count > 0 && BossRoomSpawned == true)
                {
                    newRoom = (RoomModule)Instantiate(DeadEnd);
                    newRoomExits = newRoom.GetExitsForRoom();
                    exitMatch = newRoomExits.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newRoomExits);
                    MatchExit(pendExit, exitMatch);
                }
                else
                {
                    newTag = GetRandom(pendExit.connectorTags);
                    newRoomPrefab = GetRandomRoom(Rooms, newTag);

                    newRoom = (RoomModule)Instantiate(newRoomPrefab);
                    newRoomExits = newRoom.GetExitsForRoom();
                    exitMatch = newRoomExits.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newRoomExits);
                    MatchExit(pendExit, exitMatch);
                    newExit.AddRange(newRoomExits.Where(e => e != exitMatch));
                }
            }

            pendExits = newExit;

        }

    }

    private void MatchExit(RoomConnector oldExit, RoomConnector newExit)
    {
        var newRoom = newExit.transform.parent;
        var forwardVectToMatch = -oldExit.transform.forward;
        var correctRotate = Azimuth(forwardVectToMatch) - Azimuth(newExit.transform.forward);
        newRoom.RotateAround(newExit.transform.position, Vector3.up, correctRotate);
        var correctTranslate = oldExit.transform.position - newExit.transform.position;
        newRoom.transform.position += correctTranslate;
    }

    private static TItem GetRandom<TItem>(TItem[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    private static RoomModule GetRandomRoom(IEnumerable<RoomModule> rooms, string tagMatch)
    {
        var matchingRooms = rooms.Where(r => r.RoomTags.Contains(tagMatch)).ToArray();
        return GetRandom(matchingRooms);
    }

    private static float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }
}
