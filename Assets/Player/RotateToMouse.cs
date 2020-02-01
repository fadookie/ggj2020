using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        float AngleRad = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, transform.lossyScale.x > 0 ? AngleDeg : (AngleDeg + 180));
    }
}
