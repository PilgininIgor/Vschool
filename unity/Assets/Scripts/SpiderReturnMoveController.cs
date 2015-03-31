using UnityEngine;

public class SpiderReturnMoveController : MonoBehaviour
{

    public MovementMotor motor;

    // Private memeber data
    private AI ai;

    private Transform character;
    private Vector3 spawnPos;
    public MonoBehaviour animationBehaviour;

    void Awake()
    {
        character = motor.transform;
        ai = transform.parent.GetComponentInChildren<AI>();
        spawnPos = character.position;
    }

    void Update()
    {
        motor.movementDirection = spawnPos - character.position;
        motor.movementDirection.y = 0;
        if (motor.movementDirection.sqrMagnitude > 1)
            motor.movementDirection = motor.movementDirection.normalized;

        if (motor.movementDirection.sqrMagnitude < 0.01)
        {
            character.position = new Vector3(spawnPos.x, character.position.y, spawnPos.z);
            motor.rigidbody.velocity = Vector3.zero;
            motor.rigidbody.angularVelocity = Vector3.zero;
            motor.movementDirection = Vector3.zero;
            enabled = false;
            animationBehaviour.enabled = false;
        }
    }
}
