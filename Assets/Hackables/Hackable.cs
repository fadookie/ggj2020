using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hackable : MonoBehaviour
{
    public bool isHacked;
    SpriteRenderer rend;

    Color _origColor;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        _origColor = rend.color;
    }

    // Update is called once per frame
    void Update()
    {
        rend.color = isHacked ? Color.white : _origColor;
    }
}
