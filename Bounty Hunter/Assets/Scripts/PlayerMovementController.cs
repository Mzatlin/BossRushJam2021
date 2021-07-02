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
    IPlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<IMovePhysics>();
        stats = GetComponent<IPlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stats != null && stats.GetPlayerReadiness())
        {
            CalculateMovement();
        }
        else
        {
            physics.SetMoveVelocity(Vector2.zero);
        }

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
