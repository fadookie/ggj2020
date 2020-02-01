using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using EcsRx.Unity;
using EcsRx.Unity.Components;
using UnityEngine;
using UniRx;
using Assets.EcsRx.Unity.Extensions;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Unity.MonoBehaviours;
using EcsRx.Unity.Systems;

using RuleEngine.Components;

public class MainApplication : EcsRxApplication
{
    protected override void ApplicationStarting() {
        Debug.Log("ApplicationStarting");
        RegisterAllBoundSystems();
    }

    protected override void ApplicationStarted() {
        Debug.Log("ApplicationStarted");
        //var defaultPool = PoolManager.GetPool();
        //var viewEntity = defaultPool.CreateEntity();
        //viewEntity.AddComponent(new ViewComponent());
        //viewEntity.AddComponent(new PlayerControlledComponent());
        //viewEntity.AddComponent(new CameraFollowsComponent());
    }

    public void SpawnPlayer() {
        var defaultPool = PoolManager.GetPool();
        var viewEntity = defaultPool.CreateEntity();
        viewEntity.AddComponent(new ViewComponent { DestroyWithView = true });
        viewEntity.AddComponent(new PlayerControlledComponent());
    }


    public void DestroyEntities(IEntity[] entities) {
        foreach (var entity in entities) {
            if (entity.HasComponent<ViewComponent>()) {
                var view = entity.GetComponent<ViewComponent>();
                if (view.View == null) {
                    Debug.Log("MA.DestroyEntities skipping due to null view");
                    continue;
                }
                if (view.DestroyWithView) {
                    Debug.LogFormat("MA.DestroyEntities Destroying GameObject '{0}'", view.View.name);
                    Destroy(view.View);
                } else {
                    Debug.LogFormat("MA.DestroyEntities Destroying EntityView on '{0}'", view.View.name);
                    Destroy(view.View.GetComponent<EntityView>());
                }
            }
        }
        
        PoolManager.GetPool().RemoveEntities(entities);
        
        foreach (var entity in entities) {
           entity.Dispose(); 
        }
    }
}
