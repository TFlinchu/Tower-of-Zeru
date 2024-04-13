using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private HashSet<int> clearedRooms = new HashSet<int>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public bool IsRoomCleared(int roomNumber)
    {
        return clearedRooms.Contains(roomNumber);
    }

    public void ClearRoom(int roomNumber)
    {
        clearedRooms.Add(roomNumber);
    }
}