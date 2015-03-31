using UnityEngine;

public class MechAttackMoveController : MonoBehaviour
{

    public MovementMotor motor;
    public Transform head, character, player;

    public float targetDistanceMin = 3.0f, targetDistanceMax = 4.0f,
        fireFrequency = 2, nextRaycastTime = 0, lastRaycastSuccessfulTime = 0,
        noticeTime = 0, lastFireTime = -1;

    public MonoBehaviour[] weaponBehaviours;

    // Private memeber data
    private AI ai;

    private bool inRange, firing;
    private int nextWeaponToFire = 0;

    void Awake()
    {
        character = motor.transform;
        player = GameObject.FindWithTag("Player").transform;
        ai = transform.parent.GetComponentInChildren<AI>();
    }

    void OnEnable()
    {
        inRange = false;
        nextRaycastTime = Time.time + 1;
        lastRaycastSuccessfulTime = Time.time;
        noticeTime = Time.time;
    }

    void OnDisable()
    {
        Shoot(false);
    }

    void Shoot(bool state)
    {
        firing = state;
    }

    void Fire()
    {
        if (weaponBehaviours[nextWeaponToFire])
        {
            weaponBehaviours[nextWeaponToFire].SendMessage("Fire");
            nextWeaponToFire = (nextWeaponToFire + 1) % weaponBehaviours.Length;
            lastFireTime = Time.time;
        }
    }

    void Update()
    {
        // Calculate the direction from the player to this character
        var playerDirection = (player.position - character.position);
        playerDirection.y = 0;
        var playerDist = playerDirection.magnitude;
        playerDirection /= playerDist;

        // Set this character to face the player,
        // that is, to face the direction from this character to the player
        motor.facingDirection = playerDirection;

        // For a short moment after noticing player,
        // only look at him but don't walk towards or attack yet.
        if (Time.time < noticeTime + 1.5)
        {
            motor.movementDirection = Vector3.zero;
            return;
        }

        if (inRange && playerDist > targetDistanceMax)
            inRange = false;
        if (!inRange && playerDist < targetDistanceMin)
            inRange = true;

        if (inRange)
            motor.movementDirection = Vector3.zero;
        else
            motor.movementDirection = playerDirection;

        if (Time.time > nextRaycastTime)
        {
            nextRaycastTime = Time.time + 1;
            if (ai.CanSeePlayer())
            {
                lastRaycastSuccessfulTime = Time.time;
                if (IsAimingAtPlayer())
                    Shoot(true);
                else
                    Shoot(false);
            }
            else
            {
                Shoot(false);
                if (Time.time > lastRaycastSuccessfulTime + 5)
                {
                    ai.OnLostTrack();
                }
            }
        }

        if (firing)
        {
            if (Time.time > lastFireTime + 1 / fireFrequency)
            {
                Fire();
            }
        }
    }

    bool IsAimingAtPlayer()
    {
        var playerDirection = (player.position - head.position);
        playerDirection.y = 0;
        return Vector3.Angle(head.forward, playerDirection) < 15;
    }
}
