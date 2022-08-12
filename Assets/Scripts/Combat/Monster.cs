using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Monster : MonoBehaviour
{
    public int[] num;
    List<int> numRandom = new List<int>();

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
        num = new int[3];
        for (int n = 1; n < 7; n++)    
        {
            numRandom.Add(n);
        }

        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, numRandom.Count - 1);   
            num[i] = numRandom[index];
            numRandom.RemoveAt(index);
        }

    }
}
