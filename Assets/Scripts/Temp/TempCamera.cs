using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCamera : MonoBehaviour
{
    private Vector3 originalPos;
    private Vector3 cameraOffset;
    public GameObject targetPlayer;
    float lerpDuration = 3f;
    float timer;

    private bool startLerp;


    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - targetPlayer.transform.position;
        originalPos = transform.position;
        startLerp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
           transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * 0.03f, 0f, 0f);
        }
        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            transform.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * 0.03f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            startLerp = true;
            Debug.Log("Space pressed");
        }

        if (startLerp)
        {
            timer += Time.deltaTime;
            float t = timer / lerpDuration;
            t = t * t * (3f - 2f * t);
            Vector3 endPosition = targetPlayer.transform.position + cameraOffset;
            transform.position = Vector3.Lerp(transform.position, endPosition, t);

            if (transform.position == endPosition)
            {
                Debug.Log("Camera move back");
            }
        }
    }
}
