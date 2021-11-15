using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    IMovePhysics physics;
    IPlayerStats stats;
    IMoveDirection direction;

    Vector2 dashDirection = Vector2.zero;
    Collider2D playerCollider;
    Rigidbody2D rb;
    Animator animate;
    AudioManager dashAudio;
    Vector2 imagePos = Vector2.zero;
    float distanceBetweenImages = 0.5f;

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
        dashAudio = FindObjectOfType<AudioManager>();
        physics = GetComponent<IMovePhysics>();
        stats = GetComponent<IPlayerStats>();
        direction = GetComponent<IMoveDirection>();
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponentInChildren<Animator>();
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
        animate.SetBool("IsDashing", isdashing);
    }

    void HandleDash()
    {
        if (remainingDashTime > 0)
        {
            SetPlayerState(false);
            remainingDashTime -= Time.deltaTime;
            transform.position += (Vector3)(direction.LastMovementDirection.normalized * dashSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position,imagePos) > distanceBetweenImages)
            {
                GameObject instance = ObjectPooler.Instance.GetFromPool("AfterImage");
                imagePos = transform.position;
            }
        }

        if (remainingDashTime <= 0 || !IsColliding())
        {
            isdashing = false;
            SetPlayerState(true);
        }

    }

    void AttemptDash()
    {
        if (!isdashing)
        {
            if(dashAudio != null) { dashAudio.PlayAudioByString("Play_PlayerDash", gameObject); }
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
        dashDirection = direction.LastMovementDirection;
    }

    bool IsColliding()
    {
        Ray2D ray = new Ray2D(transform.position, dashDirection);
        //Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1f, layer);
        if (hit)
        {
            return false;
        }
        return true;
    }
}
