using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int turn;
    private PhotonView photonView;

    public Text turnIndicatorText;
    
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        turn = 1;
        photonView = GetComponent<PhotonView>();
        changeTurnIndicatorText();
    }
    private void Update()
    {
        if (Player.changeTurn)
        {
            Player.changeTurn = false;
            Debug.Log("Change Turn");
            CallChangeTurn();
        }
    }

    void CallChangeTurn()
    {
        photonView.RPC("ChangeTurn", RpcTarget.All);
    }

    [PunRPC]   
    void ChangeTurn()
    {
        if (turn == 3)
        {
            turn = 1;
        }
        else
        {
            turn += 1;
        }

        changeTurnIndicatorText();
    }

    void changeTurnIndicatorText()
    {
        if (instance.turn == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            turnIndicatorText.text = "Your turn";
        }
        else if (turn == 1)
        {
            turnIndicatorText.text = "Player1's turn";
        }
        else if (turn == 2)
        {
            turnIndicatorText.text = "Player2's turn";
        }
        else if (turn == 3)
        {
            turnIndicatorText.text = "Player3's turn";
        }
    }
}
