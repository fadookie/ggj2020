using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RulesEngineManager : MonoBehaviour
{
    public GameObject player;
    public GameObject key;
    
    private List<ISystem> systems;
    private List<GameObject> entities;

    // Start is called before the first frame update
    void Start()
    {
        systems = new List<ISystem>();
        entities = new List<GameObject>();
        
        // Initialize systems in priority order
        var inputSystem = new InputSystem {
            Executor = component => component.velocity.x = Input.GetAxis("Horizontal") * 0.01f
        };
        systems.Add(inputSystem);

        systems.Add(new MovementSystem());

        //Initialize entities and components 
        entities.Add(player);
        player.AddComponent<MovementComponent>();
        
        entities.Add(key);
        
        foreach (var system in systems) {
            system.Setup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var system in systems.Where(s => s.NeedsUpdateTick())) {
            system.Execute(entities);
        }
    }
}
