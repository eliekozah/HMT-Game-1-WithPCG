using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PinUIHandler : MonoBehaviour
{
    public int playerPinnedIndex; // Player that made this pin
    public int IconType; // 0: danger, 1: Assist, 2: OMW, 3: Unknown, 4: location
    public Sprite[] pinIcons = new Sprite[5]; // 0: danger, 1: Assist, 2: OMW, 3: Unknown, 4: location

    private RectTransform rectTransform;
    private Vector3 pingPosition; // 3D pin position
    private Transform Player;

    private float VisionDistance;
    private float pinDistance;

    public void SetUp(Vector3 pingPosition, int iconType, int player)
    {
        playerPinnedIndex = player;
        IconType = iconType;
        this.pingPosition = pingPosition;
        rectTransform = transform.GetComponent<RectTransform>();
        Player = GameManager.instance.MainPlayer.transform;

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            VisionDistance = 4.7f;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            VisionDistance = 10.2f;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        {
            VisionDistance = 7.4f;
        }
        //Debug.Log("Setup: " + pingPosition + " Player: " + Player);
    }

    private void Update()
    {
        pinDistance = Vector3.Distance(Player.position, this.pingPosition);
        //Debug.Log(pinDistance);
        if (pinDistance > VisionDistance) // if pin position is out of visable area, show 2D pinIcon
        {
            if (PinningSystem.pinViewEnable[playerPinnedIndex]) // if overlap with other, pinViewEnabled = true
            {
                this.GetComponent<Image>().sprite = pinIcons[IconType]; // show specific pin icon
            }
            else  // if not overlap with other
            {
                this.GetComponent<Image>().sprite = pinIcons[4]; //show location pin icon
            }
            this.GetComponent<Image>().enabled = true;
            Vector3 fromPosition = new Vector3(Player.position.x, Player.position.z, 0);
            Vector3 dir = (new Vector3(pingPosition.x, pingPosition.z, 0) - fromPosition).normalized;
            float uiRadius = 250f;
            rectTransform.anchoredPosition = dir * uiRadius;
        }
        else
        {
            this.GetComponent<Image>().enabled = false;
        }
    }
}
