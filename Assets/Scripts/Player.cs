using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    public float speed;
    public Transform movePoint;
    public int moveCount;
    public static bool changeTurn;

    PhotonView view;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        movePoint.parent = null;
        moveCount = 0;
        changeTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine && GameManager.instance.turn == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, movePoint.position) == 0f)
            {
                if (moveCount == 4)
                {
                    moveCount = 0;
                    changeTurn = true;
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 3, 0f, 0f);
                    moveCount++;
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    movePoint.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * 3);
                    moveCount++;
                }
            }
 
        }

    }
}
