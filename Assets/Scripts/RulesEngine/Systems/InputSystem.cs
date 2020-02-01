using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : ISystem
{
    public void Setup() {
    }
    
    public bool NeedsUpdateTick() {
        return false;
    }

    public void Execute(IList<GameObject> objects) {
        
    }
}
