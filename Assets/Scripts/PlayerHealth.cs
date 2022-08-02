using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    public Text playerHealthTxt;
    void Start()
    {
        health = 3;
        playerHealthTxt = GameObject.Find("UI").transform.GetChild(2).GetComponent<Text>();
    }
    private void Update()
    {
        playerHealthTxt.text = "Health: " + health.ToString();
    }
    public int Gethealth()
    {
        return health;
    }

    public void Damage()
    {
        health--;
    }
}
