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
            foreach (var entity in entities) {
                var movementComponent = entity.GetComponent<MovementComponent>();
//                if (movementComponent) {
                if (false) {
                    Destroy(movementComponent);
                } else {
                    CopyPlayerComponentIfNeeded<CharacterController2D>(entity);
                    CopyPlayerComponentIfNeeded<Rigidbody2D>(entity);
                    CopyPlayerComponentIfNeeded<BoxCollider2D>(entity);
                    var boxCollider = GetComponent<BoxCollider2D>();
                    var characterController = GetComponent<CharacterController2D>();
                    if (characterController?.m_GroundCheck == null) {
//                        var groundCheck = (GameObject)GameObject.Instantiate(null, entity.transform);
                        var groundCheck = new GameObject("Ground Check");
                        groundCheck.AddComponent<RectTransform>();
                        groundCheck.transform.parent = characterController.transform;
                        var position = Vector3.zero;
                        position.y = boxCollider.offset.y + boxCollider.size.y;
                        groundCheck.transform.localPosition = position;
                    }
                    
                    if (!entity.GetComponent<PlayerMove>()) {
                        entity.AddComponent<PlayerMove>();
                    }
                    if (!entity.GetComponent<MovementComponent>()) {
                        entity.AddComponent<MovementComponent>();
                    }
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
