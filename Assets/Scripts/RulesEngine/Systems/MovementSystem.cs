using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovementSystem : ISystem
{
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
            var transform = component.gameObject.transform.localPosition;
            transform += component.velocity;
            component.gameObject.transform.localPosition = transform;
        }
    }
}
