using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 30f;
    Vector2 movement;
    float verticalMovement;
    float horizontalMovement;

    IMovePhysics physics;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<IMovePhysics>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");
        movement = new Vector2(horizontalMovement, verticalMovement);
        movement = Vector2.ClampMagnitude(movement, 1f);
        physics.SetMoveVelocity(movement * moveSpeed);

    }
}
