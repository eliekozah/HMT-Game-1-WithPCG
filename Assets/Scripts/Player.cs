using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviour
{
    PhotonView view;
    public int playerId;
    public float speed;

    [HideInInspector] public Transform movePoint;
    [HideInInspector] public Vector3 prevMovePointPos;
    [HideInInspector] public bool[] movable; // detecting walls. index 0: left, 1: right, 2: front, 3: back 

    public int moveCount;
    public static bool changeTurn;

    private Animator animator;

    public CameraManager cameraManager;

    void Awake()
    {
        view = GetComponent<PhotonView>();

    }
    private void Start()
    {
        playerId = GameManager.instance.playerIDs.IndexOf(this.gameObject.GetPhotonView().ViewID);
        cameraManager = FindObjectOfType<CameraManager>();
        movePoint = this.transform.GetChild(1);
        movePoint.parent = null;
        prevMovePointPos = movePoint.position;
        moveCount = 0;
        changeTurn = false;
        movable = new bool[4] { true, true, true, true};

        animator = this.transform.GetChild(0).GetComponent<Animator>();
        animator.SetBool("Idle", true);
    }

    void Update()
    {
        if (view.IsMine && GameManager.instance.turn == PhotonNetwork.LocalPlayer.ActorNumber && !CombatSystem.instance.isInFight && cameraManager.cameraIsSet)
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
            
            animator.SetBool("Walk", false);
            animator.SetBool("Attact", false);
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Attact", false);
            animator.SetBool("Walk", true);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Triggered Goal");
            if (checkRightGoal(col.gameObject))
            {
                if (view.IsMine)
                {
                    GameManager.instance.CallGoalCount();
                }

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.Destroy(col.gameObject);
                }
            }
        }

        if (col.gameObject.CompareTag("Door"))
        {
            Debug.Log("Triggered Door");
            if (view.IsMine && GameManager.instance.Goal == 3)  //After collect all the goal, the door can be stepped and end game
            {
                GameManager.instance.CallEndGame();
            }
        }

        if (col.gameObject.CompareTag("Rock"))
        {
            if (view.IsMine)
            {
                playAttactAnimation();
            }
            Debug.Log("Triggered Rock");
            CombatSystem.instance.isInFight = true;
            CombatSystem.instance.StartFight(col.gameObject, 0, this.gameObject);
        }
        else if (col.gameObject.CompareTag("Trap"))
        {
            if (view.IsMine)
            {
                playAttactAnimation();
            }
            Debug.Log("Triggered Trap");
            CombatSystem.instance.isInFight = true;
            CombatSystem.instance.StartFight(col.gameObject, 1, this.gameObject);
        }
        else if (col.gameObject.CompareTag("Monster"))
        {
            if (view.IsMine)
            {
                playAttactAnimation();
            }
            Debug.Log("Triggered Monster");
            CombatSystem.instance.isInFight = true;
            CombatSystem.instance.StartFight(col.gameObject, 2, this.gameObject);
        }
    }

    private bool checkRightGoal(GameObject goal)
    {
        if (playerId == 0 && goal.name == "DwarfGoal")
        {
            return true;
        }
        else if (playerId == 1 && goal.name == "GiantGoal")
        {
            return true;
        }
        else if (playerId == 2 && goal.name == "HumanGoal")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void playAttactAnimation()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", true);
    }

}
