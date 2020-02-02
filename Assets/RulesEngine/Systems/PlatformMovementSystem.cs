﻿using System.Collections;
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
            transform.y = component.initialPosition.y + Mathf.Sin(Time.time * component.oscillationSpeed) * component.oscillationAmplitude;
            component.gameObject.transform.localPosition = transform;
        }
    }
}
