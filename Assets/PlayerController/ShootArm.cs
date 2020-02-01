using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArm : MonoBehaviour
{
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
}
