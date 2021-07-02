using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Camera cam;
    bool canDash = true;
    bool isdashing = false;
    float distance;
    Vector2 mousePos;
    Vector2 dashDirection;
    Collider2D playerCollider;
    float dashSpeed;
    IMovePhysics physics;
    IPlayerStats stats;
    [SerializeField] float maxDashSpeed = 100f;
    float timeThreshold = 0.4f;
    float dashDelay;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        dashSpeed = maxDashSpeed;
        physics = GetComponent<IMovePhysics>();
        stats = GetComponent<IPlayerStats>();
        playerCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
      
        if(Time.time > dashDelay)
        {
            canDash = true;
        }


        if (Input.GetKeyDown(KeyCode.Space) && stats != null && stats.GetPlayerReadiness())
        {
            AttemptDash();
        }
        if (isdashing)
        {
            HandleDash();
        }

    }

    void HandleDash()
    {
        distance = Vector2.Distance(mousePos, transform.position);
        if (distance < 3f || dashSpeed < 5)
        {
            isdashing = false;
            dashSpeed = maxDashSpeed;
            dashDelay = Time.time + timeThreshold;
            if (stats != null)
            {
                stats.SetPlayerReadiness(true);
            }
            if (playerCollider != null)
            {
                playerCollider.enabled = true;
            }
        }
        else
        {
            //physics.SetMoveVelocity(dashDirection * dashSpeed);
            transform.position += (Vector3)(dashDirection * dashSpeed * Time.deltaTime);
            dashSpeed -= dashSpeed * 10f * Time.deltaTime;
        }
    }

    void AttemptDash()
    {
        if (canDash && !isdashing)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            dashDirection = (mousePos - (Vector2)transform.position).normalized;
            isdashing = true;
            canDash = false;
            if(stats != null)
            {
                stats.SetPlayerReadiness(false);
            }
            if(playerCollider != null)
            {
                playerCollider.enabled = false;
            }
        }
    }
}
