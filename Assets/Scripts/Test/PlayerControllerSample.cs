using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class PlayerControllerSample : MonoBehaviour
{
    private PlayerMovementInput input;
    public int moveSpeed;
    //private CharacterController cc;
    private Rigidbody rb;

    private void Awake()
    {
        input = new PlayerMovementInput();
        input.Enable();
    }

    void Start()
    {
        //cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var currentInput = (float2)input.Player.Move.ReadValue<Vector2>();
        //var moveDirection = math.normalize(new float2(moveDirection.x, moveDirection.z));
        //var originalVelocity = myGameObject.PhysicsVelocity.Linear;
        var moveVelocity = currentInput * moveSpeed;
        rb.linearVelocity = new float3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.y);
    }
}
