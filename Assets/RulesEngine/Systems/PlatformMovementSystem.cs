using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RulesEngine.Components;

public class PlatformMovementSystem : ISystem
{
    IEnumerable<PlatformMovementComponent> GetPlatformComponents(IList<GameObject> entities) {
        return entities
            .Select(x => x.GetComponent<PlatformMovementComponent>())
            .Where(x => x);
    }
    
    public void Setup(GameObject entity) {
        var component = entity.GetComponent<PlatformMovementComponent>();
        if (component != null) {
            component.initialPosition = component.gameObject.transform.position;
        }
    }

    public bool NeedsUpdateTick() {
        return true;
    }

    public void Execute(IList<GameObject> objects) {
        var movementComponents = GetPlatformComponents(objects);
        
        foreach (var component in movementComponents) {
            var transform = component.gameObject.transform.localPosition;
            var oscillation = Mathf.Sin(Time.time * component.oscillationSpeed) * component.oscillationAmplitude;
            switch (component.oscillationDirection) {
                case OscillationDirection.Horizontal:
                    transform.x = component.initialPosition.x + oscillation;
                    break;
                case OscillationDirection.Vertical:
                    transform.y = component.initialPosition.y + oscillation;
                    break;
            }
            component.gameObject.transform.localPosition = transform;
        }
    }
}
