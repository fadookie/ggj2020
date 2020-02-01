using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UniRx;
using System.Linq;

public class InputSystem : ISystem
{
//    private IDisposable subscription;
//    
//    ~InputSystem() {
//        subscription?.Dispose();
//    }
//    
//    public void Setup() {
//        subscription = Observable.EveryUpdate()
//            .Select(_ => )
//            .Subscribe();
//    }
    
    public delegate void ExecutorDelegate(MovementComponent component);
    public ExecutorDelegate Executor { get; set; }

    public void Setup() {
    }

    public bool NeedsUpdateTick() {
        return true;
    }

    public void Execute(IList<GameObject> objects) {
        var movementComponents = objects
            .Select(x => x.GetComponent<MovementComponent>())
            .Where(x => x);
        
        foreach (var component in movementComponents) {
            Executor(component);
        }
    }
}
