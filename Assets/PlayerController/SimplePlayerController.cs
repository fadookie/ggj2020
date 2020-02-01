using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;

            if (transform.localScale.y != 1)
            {
                transform.localScale *= -1;
            }
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
            if (transform.localScale.y != -1)
            {
                transform.localScale *= -1;
            }
        }


        transform.position = pos;
    }
}
