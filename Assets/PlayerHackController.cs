using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHackController : MonoBehaviour
{
    public GameObject Beam1;
    private GameObject _beam1End;
    public GameObject Beam2;
    private GameObject _beam2End;

    public void ConnectToTerminal(Terminal termObj)
    {
        Beam1.SetActive(true);
        _beam1End = termObj.gameObject;
        UpdateLines(Beam1, _beam1End);
    }

    public void ClearTerminal()
    {
        Beam1.SetActive(false);
    }

    public void ConnectToHackable(Hackable hackObj)
    {
        Beam2.SetActive(true);
        _beam2End = hackObj.gameObject;
        UpdateLines(Beam2, _beam2End);
    }

    public void ClearHackable()
    {
        Beam2.SetActive(false);
    }

    private void Update()
    {
        if(Beam1.activeSelf)
        {
            UpdateLines(Beam1, _beam1End);
        }

        if (Beam2.activeSelf)
        {
            UpdateLines(Beam2, _beam2End);
        }
    }

    private void UpdateLines(GameObject beam, GameObject endPoint)
    {
        List<Vector3> points = new List<Vector3>();
        var line = beam.GetComponent<LineRenderer>();

        points.Add(transform.position);
        points.Add(endPoint.transform.position);

        line.SetPositions(points.ToArray());
    }
}