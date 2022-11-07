using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private PhotonView photonView;
    private GameData gameData;
    public bool isGameStart;
    public bool[] isFinishedTutorial = new bool[3] { false, false, false };

    public GameObject tilePrefabs;

    // UI
    private Text turnIndicatorText;
    private Text goalIndicatorText;
    private Text actionCountTxt;

    [HideInInspector] public int turn; // Indicate which player's turn
    public GameObject MainPlayer; 
    [HideInInspector] public List<int> playerIDs = new List<int>(); // All the players' Photon ViewID
    [HideInInspector] public int Goal; // Goal collected, shown on the UI
    [HideInInspector] public int moveLeft; // Player's remaining moves, shown on the UI

    /*    public GameObject Player1Items;
    public GameObject Player2Items;
    public GameObject Player3Items;*/

    private void Awake()
    {
        instance = this;
        playerIDs.Add(0);
        playerIDs.Add(0);
        playerIDs.Add(0);
        gameData = FindObjectOfType<GameData>();

        if (gameData.gameLevel == 1)
        {
            isGameStart = false;
        }
        else
        {
            isGameStart = true;
        }
    }
    private void Start()
    {
        turnIndicatorText = GameObject.Find("UI").GetComponentsInChildren<Text>(true)[0]; 
        goalIndicatorText = GameObject.Find("GoalCountNum").GetComponent<Text>();
        actionCountTxt = GameObject.Find("UI").GetComponentsInChildren<Text>(true)[1];
        Debug.Log(turnIndicatorText.text);
        turn = 1; // Player 1 goes first
        photonView = GetComponent<PhotonView>();
        //ChangeTurnIndicatorText();
        if (SceneManager.GetActiveScene().name == "Level_5")
        {
            SetTiles();
        }
        //setVisalbleObject();
    }
    private void Update()
    {
        if (Player.changeTurn) // if turn changed
        {
            Player.changeTurn = false;
            Debug.Log("Change Turn");
            CallChangeTurn();
        }
        actionCountTxt.text = "Action Left: " + moveLeft.ToString();
        goalIndicatorText.text = Goal.ToString();
    }

    public void CallStartGame()
    {
        photonView.RPC("StartGame", RpcTarget.All);
    }

    public void CallEndTutorial()
    {
        photonView.RPC("EndTutorial", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber);
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

    public void CallNextLevel()
    {
        if (gameData.gameLevel == 5)
        {
            CallEndGame();
        }
        else
        {
            photonView.RPC("LoadNextLevel", RpcTarget.All);
        }
    }

    [PunRPC]   
    public void ChangeTurn()
    {
        if (turn == 3) // the next turn for player 3 is player 1
        {
            turn = 1;
        }
        else
        {
            turn += 1;
        }

        ChangeTurnIndicatorText();
    }

    private void ChangeTurnIndicatorText()
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
    public void StartGame()
    {
        GameObject.Find("WaitForPlayerTxt").SetActive(false);
        turnIndicatorText.gameObject.SetActive(true);
        actionCountTxt.gameObject.SetActive(true);
        ChangeTurnIndicatorText();
        isGameStart = true;
    }
    [PunRPC]
    public void EndTutorial(int playerNum)
    {
        isFinishedTutorial[playerNum-1] = true;

        if (CheckEndTutoial())
        {
            CallStartGame();
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
        playerIDs[playerNum-1] = id;
    }

    [PunRPC]
    public void EndGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("EndGame"); 
        }
    }

    [PunRPC]
    public void LoadNextLevel()
    {
        int nextLevel = gameData.gameLevel + 1;
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Level_" + nextLevel.ToString()); // Load Next Level
        }
    }

    private void SetTiles()
    {
        GameObject tile;
        float tileDist = gameData.tileSize + gameData.tileGapLength;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tile = Instantiate(tilePrefabs, new Vector3(tileDist * i, 0.227f, tileDist * j), Quaternion.identity);
                tile.transform.parent = GameObject.Find("Tiles").transform;
            }
        }
    }

    private bool CheckEndTutoial()
    {
        bool isEnd = true;
        for (int i = 0; i < isFinishedTutorial.Length; i++)
        {
            if (!isFinishedTutorial[i])
            {
                isEnd = false;
            }
        }

        return isEnd;
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
