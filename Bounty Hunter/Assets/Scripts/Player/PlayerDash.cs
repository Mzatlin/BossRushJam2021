using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDash : MonoBehaviour
{
    IMovePhysics physics;
    IPlayerStats stats;
    IMoveDirection direction;

    Vector2 dashDirection = Vector2.zero;
    Collider2D playerCollider;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] float dashSpeed = 100f;
    [SerializeField] float dashDuration = 0.5f;
    [SerializeField] LayerMask layer;

    float remainingDashTime = 0f;
    float dashDelay = 0f;
    bool isdashing = false;
    float dashCooldown = 0.4f;


    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<IMovePhysics>();
        stats = GetComponent<IPlayerStats>();
        direction = GetComponent<IMoveDirection>();
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && stats != null && stats.GetPlayerReadiness())
        {
            if (Time.time >= (dashDelay + dashCooldown))
            {
                GetDashDirection();
                AttemptDash();
            }
        }
        if (isdashing)
        {
            HandleDash();
        }
        Ray2D ray = new Ray2D(transform.position, dashDirection);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

    }

    void HandleDash()
    {
        if (remainingDashTime > 0)
        {
            SetPlayerState(false);
            remainingDashTime -= Time.deltaTime;
            transform.position += (Vector3)(dashDirection * dashSpeed * Time.deltaTime);
            // rb.MovePosition(rb.position + (dashDirection * dashSpeed * Time.deltaTime));
            // rb.velocity = dashDirection * dashSpeed;
            //rb.velocity = Vector2.left * dashSpeed;
        }

        if (remainingDashTime <= 0 || IsColliding())
        {
            isdashing = false;
            SetPlayerState(true);
        }

    }

    void AttemptDash()
    {
        if (!isdashing)
        {
            isdashing = true;
            dashDelay = Time.time;
            remainingDashTime = dashDuration;
        }
    }

    void SetPlayerState(bool isActive)
    {
        if (stats != null)
        {
            stats.SetPlayerReadiness(isActive);
        }
        if (playerCollider != null)
        {
            playerCollider.enabled = isActive;
        }
    }

    void GetDashDirection()
    {
        dashDirection = direction.LastMovementDirection;// - (Vector2)transform.position).normalized;//(mousePos - (Vector2)transform.position).normalized;
    }

    bool IsColliding()
    {
        Ray2D ray = new Ray2D(transform.position, dashDirection);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 2f, layer);
        if (hit)
        {
            Debug.Log("Hit");
            return false;
        }
        return true;
    }
}
