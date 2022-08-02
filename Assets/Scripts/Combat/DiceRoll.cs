using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    private bool diceRolled;
    private bool diceSpinning;
    private bool endGame;
    private Vector3 orignialPosition;
    
    void Start()
    {
        endGame = false;
        diceRolled = false;
    }
    void OnEnable()
    {
        orignialPosition = this.transform.position;
        GetComponent<Rigidbody>().useGravity = false;
    }
    void OnDisable()
    {
        resetPostion();
        diceRolled = false;
        diceSpinning = false;
        endGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !diceRolled && !endGame)
        {
            diceRolled = true;
        }

        if (diceRolled)
        {
            if (!diceSpinning)
            {
                diceSpinning = true;
                this.transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                StartCoroutine("stopSpinDice");
            }
            spinDice();
        }
    }

    private void spinDice()
    {
        this.transform.Rotate(300f * Time.deltaTime, 300f * Time.deltaTime, 300f * Time.deltaTime, Space.Self);
    }

    private IEnumerator stopSpinDice()
    {
        yield return new WaitForSeconds(1.5f);
        GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(.5f);
        diceRolled = false;
        endGame = true;
        /*        yield return new WaitForSeconds(2f);
                resetPostion();
                this.gameObject.SetActive(false);*/
    }

    private void resetPostion()
    {
        this.transform.position = orignialPosition;
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        GetComponent<Rigidbody>().useGravity = false;
    }

    public void ReRoll()
    {
        resetPostion();
        diceRolled = false;
        diceSpinning = false;
        endGame = false;
        GetComponent<Rigidbody>().useGravity = false;
    }
}
