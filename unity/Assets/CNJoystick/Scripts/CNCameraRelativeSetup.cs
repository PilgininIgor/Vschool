using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class CNCameraRelativeSetup : MonoBehaviour
{
    public CNJoystick joystick;
    public float runSpeed = 3f;

    private CharacterController characterController;
    private Camera mainCamera;
    private float gravity;
    private Vector3 totalMove;
	private Animator anim;

    // This variable is only valuable if you're using Mouse as input
    // if you use only Touch input, feel free to remove this variable
    // and it's usage from this code
    private bool tweakedLastFrame;

    void Awake()
    {
		if (joystick == null)
		{
			var joyObject = GameObject.Find("Joystick-move");
			if (joyObject != null)
				joystick = joyObject.GetComponent<CNJoystick>();
		}
		if (joystick != null) {
			joystick.JoystickMovedEvent += JoystickMovedEventHandler;
			joystick.FingerLiftedEvent += StopMoving;
		}
		characterController = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		mainCamera = Camera.main;
		gravity = -Physics.gravity.y;
		totalMove = Vector3.zero;
		tweakedLastFrame = false;
    }

    /** 
     * This function is called when player lifts his finger
     */
    private void StopMoving()
    {
        totalMove = Vector3.zero;
    }

    void FixedUpdate()
    {
        if(!tweakedLastFrame)
        {
            totalMove = Vector3.zero;
        }
        if (!characterController.isGrounded)
        {
            totalMove.y = (Vector3.down * gravity).y;
        }
		if (totalMove != Vector3.zero)
		{
			anim.SetFloat("Speed", runSpeed);
			anim.speed = 1.5f;
		}
        //characterController.Move(totalMove * Time.deltaTime);
        tweakedLastFrame = false;
    }

    private void JoystickMovedEventHandler(Vector3 dragVector)
    {
        dragVector.z = dragVector.y;
        dragVector.y = 0f;
        Vector3 movement = mainCamera.transform.TransformDirection(dragVector);
        movement.y = 0f;
        // Uncomment this line if you want to normalize speed,
        // to keep the speed at a constant value
        // -- UNCOMMENT THIS ---
        movement.Normalize();
        // ---------------------
        totalMove.x = movement.x * runSpeed;
        totalMove.z = movement.z * runSpeed;
        FaceMovementDirection(movement);
        tweakedLastFrame = true;
    }

    private void FaceMovementDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.1)
        {
			anim.SetFloat("Direction", direction.x);
            transform.forward = direction;
        }
    }
}
