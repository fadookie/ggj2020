using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RulesEngineManager : MonoBehaviour
{
    public GameObject player;
    public GameObject key;
    
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

        //Initialize entities and components 
        entities.Add(player);
        player.AddComponent<MovementComponent>();
        
        entities.Add(key);
        
        foreach (var system in systems) {
            system.Setup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("s")) {
            var movementComponent = key.GetComponent<MovementComponent>();
    //                if (movementComponent) {
            if (false) {
                Destroy(movementComponent);
            } else {
                CopyPlayerComponentIfNeeded<CharacterController2D>(key);
                CopyPlayerComponentIfNeeded<Rigidbody2D>(key);
                CopyPlayerComponentIfNeeded<BoxCollider2D>(key);
                var boxCollider = key.GetComponent<BoxCollider2D>();
                var characterController = key.GetComponent<CharacterController2D>();
                if (characterController != null) {
    //                        var groundCheck = (GameObject)GameObject.Instantiate(null, entity.transform);
                    var groundCheck = new GameObject("Ground Check");
                    groundCheck.AddComponent<Transform>();
                    var position = Vector3.zero;
                    position.y = boxCollider.offset.y + boxCollider.size.y;
                    groundCheck.transform.localPosition = position;
                    groundCheck.transform.parent = characterController.transform;
                    characterController.m_GroundCheck = groundCheck.transform;
                }
                
                if (!key.GetComponent<PlayerMove>()) {
                    key.AddComponent<PlayerMove>();
                }
                if (!key.GetComponent<MovementComponent>()) {
                    key.AddComponent<MovementComponent>();
                }
            }
        }
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
