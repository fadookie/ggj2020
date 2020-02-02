using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISystem
{
    void Setup(GameObject entity);

    bool NeedsUpdateTick();
    
    void Execute(IList<GameObject> objects);
}
