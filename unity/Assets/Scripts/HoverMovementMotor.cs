using UnityEngine;

public class HoverMovementMotor : MovementMotor
{

    //public var movement : MoveController;
    public float flyingSpeed = 5.0f, flyingSnappyness = 2.0f, turningSpeed = 3.0f,
        turningSnappyness = 3.0f, bankingAmount = 1.0f;

    void FixedUpdate()
    {
        // Handle the movement of the character
        var targetVelocity = movementDirection * flyingSpeed;
        var deltaVelocity = targetVelocity - GetComponent<Rigidbody>().velocity;
        GetComponent<Rigidbody>().AddForce(deltaVelocity * flyingSnappyness, ForceMode.Acceleration);

        // Make the character rotate towards the target rotation
        var facingDir = facingDirection != Vector3.zero ? facingDirection : movementDirection;
        if (facingDir != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(facingDir, Vector3.up);
            var deltaRotation = targetRotation * Quaternion.Inverse(transform.rotation);
            Vector3 axis;
            float angle;
            deltaRotation.ToAngleAxis(out angle, out axis);
            var deltaAngularVelocity = axis * Mathf.Clamp(angle, -turningSpeed, turningSpeed) - GetComponent<Rigidbody>().angularVelocity;

            var banking = Vector3.Dot(movementDirection, -transform.right);

            GetComponent<Rigidbody>().AddTorque(deltaAngularVelocity * turningSnappyness + transform.forward * banking * bankingAmount);
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        // Move up if colliding with static geometry
        if (collisionInfo.rigidbody == null)
            GetComponent<Rigidbody>().velocity += Vector3.up * Time.deltaTime * 50;
    }

}
