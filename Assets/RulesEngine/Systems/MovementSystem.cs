using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovementSystem : ISystem
{
    public void Setup(GameObject entity) {
    }

    public bool NeedsUpdateTick() {
        return true;
    }

    public void Execute(IList<GameObject> objects) {
#warning TODO refactor some of the movement logic in PlayerMove here
//        var movementComponents = objects
//            .Select(x => x.GetComponent<MovementComponent>())
//            .Where(x => x);
//        
//        foreach (var component in movementComponents) {
//            var transform = component.gameObject.transform.localPosition;
//            transform += component.velocity;
//            component.gameObject.transform.localPosition = transform;
//        }
    }
}
