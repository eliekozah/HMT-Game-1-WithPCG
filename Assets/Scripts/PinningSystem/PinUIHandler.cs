using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PinUIHandler : MonoBehaviour
{
    public int IconType; //0: question, 1: notice
    public Sprite questionIcon;
    public Sprite noticeIcon;
    public Sprite locationIcon;

    private RectTransform rectTransform;
    private Vector3 pingPosition;
    private Transform Player;

    private float VisionDistance;
    private float pinDistance;
    public void SetUp(Vector3 pingPosition, int iconType)
    {
        IconType = iconType;
        this.pingPosition = pingPosition;
        rectTransform = transform.GetComponent<RectTransform>();
        Player = GameManager.instance.MainPlayer.transform;

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            VisionDistance = 5f;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            VisionDistance = 10f;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        {
            VisionDistance = 7.5f;
        }
        Debug.Log("Setup: " + pingPosition + " Player: " + Player);
    }

    private void Update()
    {
        pinDistance = Vector3.Distance(Player.position, this.pingPosition);
        if (pinDistance > VisionDistance)
        {
            if (PinningSystem._IsPinnable && IconType == 0)
            {
                this.GetComponent<Image>().sprite = questionIcon;
            }
            else if(PinningSystem._IsPinnable && IconType == 1)
            {
                this.GetComponent<Image>().sprite = noticeIcon;
            }
            else
            {
                this.GetComponent<Image>().sprite = locationIcon;
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
