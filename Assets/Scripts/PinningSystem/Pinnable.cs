using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pinnable : MonoBehaviour
{
    PhotonView view;
    public bool isTriggered;
    // Start is called before the first frame update
    void Awake()
    {
        view = this.GetComponentInParent<PhotonView>();
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        if (view.IsMine && col.CompareTag("VisableArea") && !isTriggered)
        {
            Debug.Log("overlap with other");
            Debug.Log(col.gameObject.name);
            PinningSystem._IsPinnable = true;
            isTriggered = true;
        }    
    }
    private void OnTriggerExit(Collider col)
    {
        if (view.IsMine && col.CompareTag("VisableArea"))
        {
            Debug.Log("overlap with other Exit");
            PinningSystem._IsPinnable = false;
            isTriggered = false;
        }
    }
}
