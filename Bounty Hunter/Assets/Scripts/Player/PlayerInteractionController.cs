using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] float interactionRange = 1f;

    IInteract interact;
    IPlayerStats player;
    IMoveDirection moveDirection;

    Ray2D ray;
    // Start is called before the first frame update
    void Start()
    {
        moveDirection = GetComponent<IMoveDirection>();
        player = GetComponent<IPlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        var hit = DrawRay();
        if (hit && player.GetPlayerReadiness())
        {
            AttemptInteraction(hit);
        }
        else
        {
            if(interact != null)
            {
                interact.IsCurrentingInteracting = false;
            }
        }
    }

    void AttemptInteraction(RaycastHit2D _hit)
    {
        interact = _hit.transform.GetComponent<IInteract>();
        if(interact != null)
        {
            interact.IsCurrentingInteracting = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                interact.ProcessInteraction();
                interact.IsCurrentingInteracting = false;
            }
        }
    }

    RaycastHit2D DrawRay()
    {
        ray = new Ray2D(transform.position, moveDirection.LastMovementDirection);
        //Debug.DrawRay(ray.origin, ray.direction, Color.green, interactionRange);
        return Physics2D.Raycast(ray.origin, ray.direction, interactionRange, interactionLayer);
    }

}
