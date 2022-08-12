using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject DwarfPlayer;
    public GameObject GiantPlayer;
    public GameObject HumanPlayer;

    public GameObject newPlayer;

    public float minX;
    public float maxX;

    private void Start()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            newPlayer =  PhotonNetwork.Instantiate(DwarfPlayer.name, new Vector3(0, -0.86f, 3.2f), Quaternion.identity);
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            newPlayer = PhotonNetwork.Instantiate(GiantPlayer.name, new Vector3(3.2f, -0.86f, 0), Quaternion.identity);
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        {
            newPlayer = PhotonNetwork.Instantiate(HumanPlayer.name, new Vector3(0, -0.86f, 0), Quaternion.identity);
        }
        GameManager.instance.CallAddPlayerID(PhotonNetwork.LocalPlayer.ActorNumber-1, newPlayer.GetPhotonView().ViewID);
        GameManager.instance.MainPlayer = newPlayer;
        GameManager.instance.MainPlayer.transform.GetChild(3).gameObject.SetActive(true); //open vision Mask
        GameManager.instance.MainPlayer.transform.GetChild(10).gameObject.SetActive(false); //close shared Mask
    }
}
