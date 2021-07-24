using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour, IMoveDirection
{
    [SerializeField] float moveSpeed = 30f;
    Vector2 movement;
    float verticalMovement;
    float horizontalMovement;
    Animator animate;
    Vector2 lastMoveDir;
    public Vector2 LastMovementDirection => lastMoveDir;


    IMovePhysics physics;
    IPlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<IMovePhysics>();
        stats = GetComponent<IPlayerStats>();
        animate = GetComponentInChildren<Animator>();
        lastMoveDir = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats != null && stats.GetPlayerReadiness())
        {
            CalculateMovement();
        }
        else
        {
            physics.SetMoveVelocity(Vector2.zero);
            animate.SetBool("IsMoving", false);
        }
        UpdateIdle();

    }

    void UpdateIdle()
    {
        if (Mathf.Abs(verticalMovement) < 0.1f && Mathf.Abs(horizontalMovement) < 0.1f)
        {
            animate.SetBool("IsMoving", false);
        }
        else
        {
            lastMoveDir = movement;
            animate.SetFloat("LastDirX", lastMoveDir.x);
            animate.SetBool("IsMoving", true);
        }
    }

    void CalculateMovement()
    {
        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");
        animate.SetFloat("XInput", horizontalMovement);
        animate.SetFloat("YInput", verticalMovement);
        movement = new Vector2(horizontalMovement, verticalMovement);
        movement = Vector2.ClampMagnitude(movement, 1f);
        physics.SetMoveVelocity(movement * moveSpeed);

    }
}
