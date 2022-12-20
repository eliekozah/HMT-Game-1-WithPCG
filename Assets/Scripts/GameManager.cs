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
    public Text goalIndicatorText;
    public Text actionCountTxt;

/*    public GameObject Player1Items;
    public GameObject Player2Items;
    public GameObject Player3Items;*/

    public GameObject MainPlayer;
    public List<int> playerIDs = new List<int>();
    public int Goal;
    public int moveLeft;



    /**/




    /**/


    private void Awake()
    {
        instance = this;
        playerIDs.Add(0);
        playerIDs.Add(0);
        playerIDs.Add(0);
    }
    private void Start()
    {
        turn = 1;
        photonView = GetComponent<PhotonView>();
        changeTurnIndicatorText();
        //setVisalbleObject();
    }
    private void Update()
    {
        if (Player.changeTurn)
        {
            Player.changeTurn = false;
            Debug.Log("Change Turn");
            CallChangeTurn();
        }
        actionCountTxt.text = "Action Left: " + moveLeft.ToString();
        goalIndicatorText.text = Goal.ToString();
    }

    public void CallChangeTurn()
    {
        photonView.RPC("ChangeTurn", RpcTarget.All);
    }
    public void CallGoalCount()
    {
        photonView.RPC("GoalCount", RpcTarget.All);
    }
    public void CallMoveLeft(int num)
    {
        photonView.RPC("MoveLeft", RpcTarget.All, num);
    }
    public void CallAddPlayerID(int playerNum, int id)
    {
        photonView.RPC("AddPlayerID", RpcTarget.All, playerNum, id);
    }
    public void CallEndGame()
    {
        photonView.RPC("EndGame", RpcTarget.All);
    }

    [PunRPC]   
    public void ChangeTurn()
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

    private void changeTurnIndicatorText()
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

    [PunRPC]
    public void GoalCount()
    {
        Goal++;
    }

    [PunRPC]
    public void MoveLeft(int num)
    {
        moveLeft = num;
    }

    [PunRPC]
    public void AddPlayerID(int playerNum, int id)
    {
        playerIDs[playerNum] = id;
    }

    [PunRPC]
    public void EndGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("EndGame");
        }
    }

    /*    private void setVisalbleObject()
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                Player1Items.SetActive(true);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
            {
                Player2Items.SetActive(true);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
            {
                Player3Items.SetActive(true);
            }
        }*/
}
