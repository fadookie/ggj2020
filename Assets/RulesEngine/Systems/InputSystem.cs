using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System.Linq;

public class InputSystem : ISystem
{
    public delegate void ExecutorDelegate(PlayerMovementComponent component);
    public ExecutorDelegate Executor { get; set; }

    public void Setup(IList<GameObject> objects) {
    }

    public bool NeedsUpdateTick() {
        return true;
    }

    public void Execute(IList<GameObject> objects) {
        var movementComponents = objects
            .Select(x => x.GetComponent<PlayerMovementComponent>())
            .Where(x => x);
        
        foreach (var component in movementComponents) {
            Executor(component);
        }
    }
}
