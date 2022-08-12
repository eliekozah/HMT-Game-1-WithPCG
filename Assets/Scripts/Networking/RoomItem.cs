using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text roomName;
    public Text roomCount;
    LobbyManager manager;

    // Update is called once per frame

    private void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string _roomName, int _roomCount)
    {
        roomName.text = _roomName;
        roomCount.text = _roomCount.ToString() + "/3";
    }

    public void OnClickItem()
    {
        manager.JoinRoom(roomName.text);
    }
}
