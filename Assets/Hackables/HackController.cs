using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackController : MonoBehaviour
{
    public GameObject HackWindow;

    Hackable _hackObj;
    Terminal _term = null;

    void Update()
    {
        // Check if an object is hackable and if so, set hack.
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                var tmpTerm = hit.collider.GetComponent<Terminal>();
                var tmpHackable = hit.collider.GetComponent<Hackable>();
                
                // clicked a Hackable
                if(tmpHackable != null)
                {
                    if(_hackObj != null && _hackObj != tmpHackable)
                    {
                        _hackObj.isHacked = false;
                        if (_term != null)
                        {
                            _term.isHacked = false;
                        }
                    }


                    _hackObj = tmpHackable;
                    _hackObj.isHacked = !_hackObj.isHacked;
                }

                // clicked a Terminal
                if(tmpTerm != null)
                {
                    _term = tmpTerm;
                    _term.isHacked = !_term.isHacked;
                }
            }
        }

        if(_hackObj != null && !_hackObj.isHacked)
        {
            _hackObj = null;
        }

        if(_term != null && !_term.isHacked)
        {
            _term = null;
        }

        HackWindow.SetActive(_hackObj != null && _term != null);
    }

    void ConnectHackable(Hackable objToHack)
    {
        _hackObj = objToHack;
        objToHack.isHacked = true;
    }

    void ConnectTerminal(Terminal objToHack)
    {
        _term = objToHack;
        objToHack.isHacked = true;
    }
}
