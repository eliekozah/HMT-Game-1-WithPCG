using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestList : MonoBehaviour
{
    List<int> numRandom = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        numRandom.Add(1);
        numRandom.Add(2);

        Debug.Log("List count: " + numRandom.Count);
        Debug.Log("List: " + numRandom[0] + numRandom[1]);
        numRandom.RemoveAt(0);
        Debug.Log("List count: " + numRandom.Count);
        Debug.Log("List: " + numRandom[0] );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
