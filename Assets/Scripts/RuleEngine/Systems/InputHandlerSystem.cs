﻿using System;
using EcsRx.Entities;
using EcsRx.Groups;
using EcsRx.Groups.Accessors;
using EcsRx.Systems;
using EcsRx.Unity.Components;
using RuleEngine.Components;
using UnityEngine;
using UniRx;
using System.Linq;

namespace RuleEngine.Systems
{
    public class InputHandlerSystem : IReactToGroupSystem
    {
        public readonly float MovementSpeed = 2.0f;

        public IGroup TargetGroup
        {
            get
            {
                return new GroupBuilder()
                    .WithComponent<ViewComponent>()
                    .WithComponent<PlayerControlledComponent>()
                    .Build();
            }
        }

        public IObservable<IGroupAccessor> ReactToGroup(IGroupAccessor @group)
        {
            return Observable.EveryUpdate().Select(x => @group);
        }

        public void Execute(IEntity entity)
        {
            var strafeMovement = 0f;
            var forardMovement = 0f;

            if (Input.GetKey(KeyCode.A)) { strafeMovement = -1.0f; }
            if (Input.GetKey(KeyCode.D)) { strafeMovement = 1.0f; }
            if (Input.GetKey(KeyCode.W)) { forardMovement = 1.0f; }
            if (Input.GetKey(KeyCode.S)) { forardMovement = -1.0f; }

            var viewComponent = entity.GetComponent<ViewComponent>();
            var transform = viewComponent.View.transform;
            var newPosition = transform.position;

            newPosition.x += strafeMovement * MovementSpeed * Time.deltaTime;
            newPosition.z += forardMovement * MovementSpeed * Time.deltaTime;

            transform.position = newPosition;
        }
    }
}
