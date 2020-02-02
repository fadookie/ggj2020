using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RulesEngine.Components;
using UnityEngine;

public class RulesEngineManager : MonoBehaviour
{
    public GameObject player;
    public GameObject hackable;
    public GameObject[] hackables;
    
    private List<ISystem> systems;
    private List<GameObject> entities;

    // Start is called before the first frame update
    void Start()
    {
        systems = new List<ISystem>();
        entities = new List<GameObject>();
        
        // Initialize systems in priority order
        var inputSystem = new InputSystem {
            Executor = component => {
                component.horizontalMove = Input.GetAxisRaw("Horizontal") * component.runSpeed;
                component.jump = Input.GetButtonDown("Jump");
            }
        };
        systems.Add(inputSystem);

        systems.Add(new MovementSystem());
        systems.Add(new PlatformMovementSystem());

        //Initialize entities and components 
        entities.Add(player);
        AddComponentToEntity<PlayerMovementComponent>(player);
        
        entities.Add(hackable);
        AddComponentToEntity<PlatformMovementComponent>(hackable);

        entities.InsertRange(entities.Count - 1, hackables);
    }

    void AddComponentToEntity<T>(GameObject entity) where T : Component {
        entity.AddComponent<T>();
        foreach (var system in systems) {
            system.Setup(entity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("h")) {
            foreach (var hackable in hackables) {
                if (hackable.GetComponent<PlatformMovementComponent>() == null) {
                    AddComponentToEntity<PlatformMovementComponent>(hackable);
                }
            }
        }
//        if (Input.GetKey("s")) {
//            var movementComponent = hackable.GetComponent<PlayerMovementComponent>();
//    //                if (movementComponent) {
//            if (false) {
//                Destroy(movementComponent);
//            } else {
//                CopyPlayerComponentIfNeeded<CharacterController2D>(hackable);
//                CopyPlayerComponentIfNeeded<Rigidbody2D>(hackable);
//                CopyPlayerComponentIfNeeded<BoxCollider2D>(hackable);
//                var boxCollider = hackable.GetComponent<BoxCollider2D>();
//                var characterController = hackable.GetComponent<CharacterController2D>();
//                if (characterController != null) {
//    //                        var groundCheck = (GameObject)GameObject.Instantiate(null, entity.transform);
//                    var groundCheck = new GameObject("Ground Check");
//                    groundCheck.AddComponent<Transform>();
//                    var position = Vector3.zero;
//                    position.y = boxCollider.offset.y + boxCollider.size.y;
//                    groundCheck.transform.localPosition = position;
//                    groundCheck.transform.parent = characterController.transform;
//                    characterController.m_GroundCheck = groundCheck.transform;
//                }
//                
//                if (!hackable.GetComponent<PlayerMove>()) {
//                    hackable.AddComponent<PlayerMove>();
//                }
//                if (!hackable.GetComponent<PlayerMovementComponent>()) {
//                    hackable.AddComponent<PlayerMovementComponent>();
//                }
//            }
//        }
//        
        foreach (var system in systems.Where(s => s.NeedsUpdateTick())) {
            system.Execute(entities);
        }
    }

    void CopyPlayerComponentIfNeeded<T>(GameObject entity) where T : Component {
        if (entity.GetComponent<T>() == null) {
            Utils.CopyComponent(player.GetComponent<T>(), entity);
        }
    }
}
