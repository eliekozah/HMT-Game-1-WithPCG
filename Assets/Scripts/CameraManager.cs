using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraManager : MonoBehaviour
{
    public GameObject Camera_1;
    public GameObject Camera_2;
    public GameObject Camera_3;

    public GameObject MainCamera;

    public Transform targetPlayer;
    public Vector3 cameraOffset;

    void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            Camera_1.SetActive(true);
            MainCamera = Camera_1;
            targetPlayer = GameObject.Find("Player1(Clone)").transform;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            Camera_2.SetActive(true);
            MainCamera = Camera_2;
            targetPlayer = GameObject.Find("Player2(Clone)").transform;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        {
            Camera_3.SetActive(true);
            MainCamera = Camera_3;
            targetPlayer = GameObject.Find("Player3(Clone)").transform;

        }
        cameraOffset = MainCamera.transform.position - targetPlayer.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MainCamera.transform.position = targetPlayer.transform.position + cameraOffset;
    }
}
