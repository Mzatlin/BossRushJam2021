using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefenseSystemBossAI : BossAIBase
{
    public List<LineRenderer> lasers = new List<LineRenderer>();
    public List<GameObject> laserPosts = new List<GameObject>();
    public List<GameObject> segmentedLasers = new List<GameObject>();

    protected override void InitializeStateMachine()
    {
        states = new Dictionary<Type, IState>()
        {
            {typeof(DefenseSystemBossIdleState), new DefenseSystemBossIdleState(this) },
             {typeof(LaserSpinState), new LaserSpinState(this) },
              {typeof(CompleteCircleFirePatternState), new CompleteCircleFirePatternState(this) },
        };

        ResetStateMachineStates(states, 50);
        SetupLineRenderers();
    }

    void SetupLineRenderers()
    {
        foreach (Transform obj in transform)
        {
            var render = obj.gameObject.GetComponent<LineRenderer>();
            if (render != null)
            {
                lasers.Add(render);
            }
        }
    }

    public LayerMask GetObstacleMask()
    {
        return obstacleLayers;
    }

    public Quaternion SetupBullet(GameObject bullet, Vector2 direction)
    {
        float bulletangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunRotation.eulerAngles = new Vector3(0, 0, bulletangle);
        var projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetBulletDirection(direction);
        }
        return gunRotation;
    }

    protected override void SetLasersActive(bool toggle)
    {
        foreach (LineRenderer render in lasers)
        {
            render.enabled = toggle;
        }
    }

}
