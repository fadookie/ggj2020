using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Transform spawnPoint;
    public bool hasKey;

    [SerializeField] private GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        Player.transform.position = spawnPoint.position;
    }

    public void PickupKey()
    {
        hasKey = true;
    }
}
