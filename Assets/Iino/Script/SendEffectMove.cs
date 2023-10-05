using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEffectMove : MonoBehaviour
{
    public Vector3 endCoordinate; //I—¹À•W
    public float moveTime = 1.0f; //ˆÚ“®‚É‚©‚©‚éŽžŠÔ(•b)

    private float startTime;
    private bool isMoving = false;

    public void StartMoving(Vector3 endCoordinate)
    {
        this.endCoordinate = endCoordinate;
        isMoving = true;
        startTime = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            float t = (Time.time - startTime) / moveTime;
            transform.position = Vector3.Lerp(transform.position, endCoordinate, t);
            if(t >= 1.0f)
            {
                isMoving = false;
                transform.position = endCoordinate;
            }
        }

        if(transform.position == endCoordinate)
        {
            Destroy(gameObject,1f);
        }
    }
}
