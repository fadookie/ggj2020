using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RulesEngineManager : MonoBehaviour
{
    private List<ISystem> systems;
    private List<GameObject> entities;
    
    // Start is called before the first frame update
    void Start()
    {
        systems = new List<ISystem>();
        entities = new List<GameObject>();
        
        systems.Add(new InputSystem());
        systems.Add(new MovementSystem());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var system in systems.Where(s => s.NeedsUpdateTick())) {
            system.Execute(entities);
        }
    }
}
