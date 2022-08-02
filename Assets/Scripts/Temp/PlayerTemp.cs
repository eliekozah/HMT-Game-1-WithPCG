using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemp : MonoBehaviour
{

    public float speed;
    [HideInInspector] public Transform movePoint;
    [HideInInspector] public Vector3 prevMovePointPos;

    private Transform targetPlayer;
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        movePoint = this.transform.GetChild(0);
        movePoint.parent = null;
        prevMovePointPos = movePoint.position;
        targetPlayer = this.transform;
        cameraOffset = Camera.main.transform.position - targetPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TempCombat.instance.isInFight)
        {
            playerMovement();
        }
    }
    void LateUpdate()
    {
        Camera.main.transform.position = targetPlayer.transform.position + cameraOffset;
    }
    private void playerMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) == 0f)
        {
            prevMovePointPos = movePoint.position;
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    if (this.transform.rotation != Quaternion.Euler(0, 270, 0))
                    {
                        this.transform.rotation = Quaternion.Euler(0, 270, 0);
                    }
                }
                else
                {
                    if (this.transform.rotation != Quaternion.Euler(0, 90, 0))
                    {
                        this.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                }
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 3, 0f, 0f);
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    if (this.transform.rotation != Quaternion.Euler(0, 180, 0))
                    {
                        this.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                }
                else
                {
                    if (this.transform.rotation != Quaternion.Euler(0, 0, 0))
                    {
                        this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                movePoint.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * 3);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(this.transform.name);
        Debug.Log("Triggered: " + col.gameObject.name);
        if (col.gameObject.CompareTag("Rock"))
        {
            TempCombat.instance.isInFight = true;
            TempCombat.instance.StartFight(col.gameObject, 0);
        }
        else if (col.gameObject.CompareTag("Trap"))
        {
            TempCombat.instance.isInFight = true;
            TempCombat.instance.StartFight(col.gameObject, 1);
        }
        else if (col.gameObject.CompareTag("Monster"))
        {
            TempCombat.instance.isInFight = true;
            TempCombat.instance.StartFight(col.gameObject, 2);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exit");
    }
}
