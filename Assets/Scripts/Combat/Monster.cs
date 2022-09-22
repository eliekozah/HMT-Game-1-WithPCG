using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int monsterType; // L_Monster is 1, M_Monster is 2, S_Monster is 3
    
    [HideInInspector]public int[] num;
    [HideInInspector] List<int> numRandom = new List<int>();

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
        num = new int[monsterType];
        for (int n = 1; n < 7; n++)    
        {
            numRandom.Add(n);
        }

        for (int i = 0; i < monsterType; i++)
        {
            int index = Random.Range(0, numRandom.Count - 1);   
            num[i] = numRandom[index];
            numRandom.RemoveAt(index);
        }

    }
}
