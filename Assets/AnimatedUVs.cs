using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedUVs : MonoBehaviour
{
    public int uvAnimationTileY = 8;

    public float framesPerSecond = 1200.0f;

    void Update()
    {
        var line = GetComponent<LineRenderer>();
        GetComponent<LineRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0, Mathf.FloorToInt(Time.time * (framesPerSecond / uvAnimationTileY)) % uvAnimationTileY * 0.125f));
        GetComponent<LineRenderer>().material.SetTextureScale("_MainTex", new Vector2(Vector3.Magnitude(line.GetPosition(0)-line.GetPosition(1)) / line.startWidth, 0.125f));
    }
}
