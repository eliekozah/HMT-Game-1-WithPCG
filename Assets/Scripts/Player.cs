using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviour
{
    public float speed;

    [HideInInspector] public Transform movePoint;
    [HideInInspector] public Vector3 prevMovePointPos;
    [HideInInspector] public bool[] movable; // detecting walls. index 0: left, 1: right, 2: front, 3: back 

    public int moveCount;
    public static bool changeTurn;

    PhotonView view;

    void Awake()
    {
        view = GetComponent<PhotonView>();
    }
    private void Start()
    {
        movePoint = this.transform.GetChild(1);
        movePoint.parent = null;
        prevMovePointPos = movePoint.position;
        moveCount = 0;
        changeTurn = false;
        movable = new bool[4] { true, true, true, true};
    }

    void Update()
    {
        if (view.IsMine && GameManager.instance.turn == PhotonNetwork.LocalPlayer.ActorNumber && !CombatSystem.instance.isInFight)
        {
            playerMovement();
        }
    }

    private void playerMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) == 0f)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                GameManager.instance.CallMoveLeft(6 - moveCount);
            }
            else
            {
                GameManager.instance.CallMoveLeft(4 - moveCount);
            }

            prevMovePointPos = movePoint.position;
            if (PhotonNetwork.LocalPlayer.ActorNumber != 1 && moveCount == 4) // Giant & Human only has 4 actions
            {
                moveCount = 0;
                changeTurn = true;
            }
            else if (moveCount == 6) // Dwarf  has 6 actions
            {
                moveCount = 0;
                changeTurn = true;
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (Input.GetAxisRaw("Horizontal") < 0 && movable[0])  //left
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 3 - 0.2f, 0f, 0f);
                    if (this.transform.GetChild(0).rotation != Quaternion.Euler(0, 270, 0))
                    {
                        this.transform.GetChild(0).rotation = Quaternion.Euler(0, 270, 0);
                    }
                    moveCount++;
                }
                else if(Input.GetAxisRaw("Horizontal") > 0 && movable[1]) //right
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 3 + 0.2f, 0f, 0f);
                    if (this.transform.GetChild(0).rotation != Quaternion.Euler(0, 90, 0))
                    {
                        this.transform.GetChild(0).rotation = Quaternion.Euler(0, 90, 0);
                    }
                    moveCount++;
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (Input.GetAxisRaw("Vertical") < 0 && movable[3])  // back
                {
                    movePoint.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * 3 - 0.2f);
                    if (this.transform.GetChild(0).rotation != Quaternion.Euler(0, 180, 0))
                    {
                        this.transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
                    }
                    moveCount++;
                }
                else if (Input.GetAxisRaw("Vertical") > 0 && movable[2])  // front
                {
                    movePoint.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * 3 + 0.2f);
                    if (this.transform.GetChild(0).rotation != Quaternion.Euler(0, 0, 0))
                    {
                        this.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
                    }
                    moveCount++;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Triggered Goal");
            if (checkRightGoal(col.gameObject))
            {
                GameManager.instance.CallGoalCount();
                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.Destroy(col.gameObject);
                }
            }
        }

        if (col.gameObject.CompareTag("Rock"))
        {
            Debug.Log("Triggered Rock");
            Debug.Log("postion: " + this.transform.position);
            CombatSystem.instance.isInFight = true;
            CombatSystem.instance.StartFight(col.gameObject, 0, this.gameObject);
        }
        else if (col.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Triggered Trap");
            CombatSystem.instance.isInFight = true;
            CombatSystem.instance.StartFight(col.gameObject, 1, this.gameObject);
        }
        else if (col.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Triggered Monster");
            CombatSystem.instance.isInFight = true;
            CombatSystem.instance.StartFight(col.gameObject, 2, this.gameObject);
        }
    }

    private bool checkRightGoal(GameObject goal)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1 && goal.name == "DwarfGoal")
        {
            return true;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2 && goal.name == "GiantGoal")
        {
            return true;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3 && goal.name == "HumanGoal")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
