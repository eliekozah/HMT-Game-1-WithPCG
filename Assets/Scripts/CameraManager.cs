using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraManager : MonoBehaviour
{

    public static Camera MainCamera;

    private Transform targetPlayer;
    private Vector3 cameraOffset;

    void Start()
    {
        MainCamera = Camera.main;
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            MainCamera.transform.position = new Vector3(0f, 16.2f, -11.64f);
            //targetPlayer = GameObject.Find("Human(Clone)").transform;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            MainCamera.transform.position = new Vector3(3.2f, 36.5f, -34.4f);
            //targetPlayer = GameObject.Find("Giant(Clone)").transform;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        {
            MainCamera.transform.position = new Vector3(0f, 27.7f, -25.1f);
            //targetPlayer = GameObject.Find("Dwarf(Clone)").transform;
        }
        targetPlayer = GameManager.instance.MainPlayer.transform;
        cameraOffset = MainCamera.transform.position - targetPlayer.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MainCamera.transform.position = targetPlayer.transform.position + cameraOffset;
    }
}
