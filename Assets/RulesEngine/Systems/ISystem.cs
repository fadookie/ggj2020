using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISystem
{
    void Setup(IList<GameObject> objects);

    bool NeedsUpdateTick();
    
    void Execute(IList<GameObject> objects);
}
