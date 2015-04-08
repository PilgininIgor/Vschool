﻿using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{

    // Objects to drag in
    public MovementMotor motor;
    public Transform character;
    public GameObject cursorPrefab, joystickPrefab;

    // Settings
    public float cameraSmoothing = 0.01f, cameraPreview = 2.0f;

    // Cursor settings
    public float cursorPlaneHeight = 0, cursorFacingCamera = 0,
        cursorSmallerWithDistance = 0, cursorSmallerWhenClose = 1;

    // Private memeber data
    private Camera mainCamera;

    private Transform cursorObject;
    private Joystick joystickLeft, joystickRight;

    private Transform mainCameraTransform;
    private Vector3 cameraVelocity = Vector3.zero, cameraOffset = Vector3.zero, initOffsetToPlayer;

    // Prepare a cursor point varibale. This is the mouse position on PC and controlled by the thumbstick on mobiles.
    private Vector3 cursorScreenPosition;

    private Plane playerMovementPlane;

    private GameObject joystickRightGO;

    private Quaternion screenMovementSpace;
    private Vector3 screenMovementForward, screenMovementRight;

    void Awake() {		
	motor.movementDirection = Vector2.zero;
	motor.facingDirection = Vector2.zero;
	
	// Set main camera
	mainCamera = Camera.main;
	mainCameraTransform = mainCamera.transform;
	
	// Ensure we have character set
	// Default to using the transform this component is on
	if (!character)
		character = transform;
	
	initOffsetToPlayer = mainCameraTransform.position - character.position;
	
	#if UNITY_IPHONE || UNITY_ANDROID
		if (joystickPrefab) {
			// Create left joystick
			var joystickLeftGO = Instantiate (joystickPrefab) as GameObject;
			joystickLeftGO.name = "Joystick Left";
			joystickLeft = joystickLeftGO.GetComponent<Joystick> ();
			
			// Create right joystick
			joystickRightGO = Instantiate (joystickPrefab) as GameObject;
			joystickRightGO.name = "Joystick Right";
			joystickRight = joystickRightGO.GetComponent<Joystick> ();			
		}
	#elif !UNITY_FLASH
		if (cursorPrefab) {
			cursorObject = (Instantiate (cursorPrefab) as GameObject).transform;
		}
	#endif
	
	// Save camera offset so we can use it in the first frame
	cameraOffset = mainCameraTransform.position - character.position;
	
	// Set the initial cursor position to the center of the screen
	cursorScreenPosition = new Vector3 (0.5f * Screen.width, 0.5f * Screen.height, 0);
	
	// caching movement plane
	playerMovementPlane = new Plane (character.up, character.position + character.up * cursorPlaneHeight);
}

    void Start()
    {
#if UNITY_IPHONE || UNITY_ANDROID
        // Move to right side of screen
        var guiTex = joystickRightGO.GetComponent<GUITexture>();
        var t = guiTex.pixelInset;
        t.x = Screen.width - t.x - guiTex.pixelInset.width;
        guiTex.pixelInset = t;
#endif

        // it's fine to calculate this on Start () as the camera is static in rotation

        screenMovementSpace = Quaternion.Euler(0, mainCameraTransform.eulerAngles.y, 0);
        screenMovementForward = screenMovementSpace * Vector3.forward;
        screenMovementRight = screenMovementSpace * Vector3.right;
    }

    void OnDisable()
    {
        if (joystickLeft != null)
            joystickLeft.enabled = false;

        if (joystickRight != null)
            joystickRight.enabled = false;
    }

    void OnEnable()
    {
        if (joystickLeft != null)
            joystickLeft.enabled = true;

        if (joystickRight != null)
            joystickRight.enabled = true;
    }

    void Update()
    {
        // HANDLE CHARACTER MOVEMENT DIRECTION
#if UNITY_IPHONE || UNITY_ANDROID
        motor.movementDirection = joystickLeft.position.x * screenMovementRight + joystickLeft.position.y * screenMovementForward;
#else
		motor.movementDirection = Input.GetAxis ("Horizontal") * screenMovementRight + Input.GetAxis ("Vertical") * screenMovementForward;
#endif

        // Make sure the direction vector doesn't exceed a length of 1
        // so the character can't move faster diagonally than horizontally or vertically
        if (motor.movementDirection.sqrMagnitude > 1)
            motor.movementDirection.Normalize();


        // HANDLE CHARACTER FACING DIRECTION AND SCREEN FOCUS POINT

        // First update the camera position to take into account how much the character moved since last frame
        //mainCameraTransform.position = Vector3.Lerp (mainCameraTransform.position, character.position + cameraOffset, Time.deltaTime * 45.0f * deathSmoothoutMultiplier);

        // Set up the movement plane of the character, so screenpositions
        // can be converted into world positions on this plane
        //playerMovementPlane = new Plane (Vector3.up, character.position + character.up * cursorPlaneHeight);

        // optimization (instead of newing Plane):

        playerMovementPlane.normal = character.up;
        playerMovementPlane.distance = -character.position.y + cursorPlaneHeight;

        // used to adjust the camera based on cursor or joystick position

        var cameraAdjustmentVector = Vector3.zero;

#if UNITY_IPHONE || UNITY_ANDROID

        // On mobiles, use the thumb stick and convert it into screen movement space
        motor.facingDirection = joystickRight.position.x * screenMovementRight + joystickRight.position.y * screenMovementForward;

        cameraAdjustmentVector = motor.facingDirection;

#else
	
#if !UNITY_EDITOR && (UNITY_XBOX360 || UNITY_PS3)

			// On consoles use the analog sticks
			float axisX = Input.GetAxis("LookHorizontal");
			float axisY = Input.GetAxis("LookVertical");
			motor.facingDirection = axisX * screenMovementRight + axisY * screenMovementForward;
	
			cameraAdjustmentVector = motor.facingDirection;		
		
#else
	
			// On PC, the cursor point is the mouse position
			var cursorScreenPosition = Input.mousePosition;
						
			// Find out where the mouse ray intersects with the movement plane of the player
			var cursorWorldPosition = ScreenPointToWorldPointOnPlane (cursorScreenPosition, playerMovementPlane, mainCamera);
			
			float halfWidth = Screen.width / 2.0f;
			float halfHeight = Screen.height / 2.0f;
			float maxHalf = Mathf.Max (halfWidth, halfHeight);
			
			// Acquire the relative screen position			
			var posRel = cursorScreenPosition - new Vector3 (halfWidth, halfHeight, cursorScreenPosition.z);		
			posRel.x /= maxHalf; 
			posRel.y /= maxHalf;
						
			cameraAdjustmentVector = posRel.x * screenMovementRight + posRel.y * screenMovementForward;
			cameraAdjustmentVector.y = (float) 0.0;	
									
			// The facing direction is the direction from the character to the cursor world position
			motor.facingDirection = (cursorWorldPosition - character.position);
			motor.facingDirection.y = 0;			
			
			// Draw the cursor nicely
			HandleCursorAlignment (cursorWorldPosition);
			
#endif
		
#endif

        // HANDLE CAMERA POSITION

        // Set the target position of the camera to point at the focus point
        var cameraTargetPosition = character.position + initOffsetToPlayer + cameraAdjustmentVector * cameraPreview;

        // Apply some smoothing to the camera movement
        mainCameraTransform.position = Vector3.SmoothDamp(mainCameraTransform.position, cameraTargetPosition, ref cameraVelocity, cameraSmoothing);

        // Save camera offset so we can use it in the next frame
        cameraOffset = mainCameraTransform.position - character.position;
    }

    public static Vector3 PlaneRayIntersection(Plane plane, Ray ray)
    {
        float dist;
        plane.Raycast(ray, out dist);
        return ray.GetPoint(dist);
    }

    public static Vector3 ScreenPointToWorldPointOnPlane(Vector3 screenPoint, Plane plane, Camera camera)
    {
        // Set up a ray corresponding to the screen position
        var ray = camera.ScreenPointToRay(screenPoint);

        // Find out where the ray intersects with the plane
        return PlaneRayIntersection(plane, ray);
    }

    void HandleCursorAlignment(Vector3 cursorWorldPosition)
    {
        if (!cursorObject)
            return;

        // HANDLE CURSOR POSITION

        // Set the position of the cursor object
        cursorObject.position = cursorWorldPosition;

#if !UNITY_FLASH
        // Hide mouse cursor when within screen area, since we're showing game cursor instead
        Screen.showCursor = (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height);
#endif


        // HANDLE CURSOR ROTATION

        var cursorWorldRotation = cursorObject.rotation;
        if (motor.facingDirection != Vector3.zero)
            cursorWorldRotation = Quaternion.LookRotation(motor.facingDirection);

        // Calculate cursor billboard rotation
        var cursorScreenspaceDirection = Input.mousePosition - mainCamera.WorldToScreenPoint(transform.position + character.up * cursorPlaneHeight);
        cursorScreenspaceDirection.z = 0;
        var cursorBillboardRotation = mainCameraTransform.rotation * Quaternion.LookRotation(cursorScreenspaceDirection, -Vector3.forward);

        // Set cursor rotation
        cursorObject.rotation = Quaternion.Slerp(cursorWorldRotation, cursorBillboardRotation, cursorFacingCamera);


        // HANDLE CURSOR SCALING

        // The cursor is placed in the world so it gets smaller with perspective.
        // Scale it by the inverse of the distance to the camera plane to compensate for that.
        float compensatedScale = 0.1f * Vector3.Dot(cursorWorldPosition - mainCameraTransform.position, mainCameraTransform.forward);

        // Make the cursor smaller when close to character
        float cursorScaleMultiplier = Mathf.Lerp(0.7f, 1, Mathf.InverseLerp(0.5f, 4, motor.facingDirection.magnitude));

        // Set the scale of the cursor
        cursorObject.localScale = Vector3.one * Mathf.Lerp(compensatedScale, 1, cursorSmallerWithDistance) * cursorScaleMultiplier;

        // DEBUG - REMOVE LATER
        if (Input.GetKey(KeyCode.O)) cursorFacingCamera += Time.deltaTime * 0.5f;
        if (Input.GetKey(KeyCode.P)) cursorFacingCamera -= Time.deltaTime * 0.5f;
        cursorFacingCamera = Mathf.Clamp01(cursorFacingCamera);

        if (Input.GetKey(KeyCode.K)) cursorSmallerWithDistance += Time.deltaTime * 0.5f;
        if (Input.GetKey(KeyCode.L)) cursorSmallerWithDistance -= Time.deltaTime * 0.5f;
        cursorSmallerWithDistance = Mathf.Clamp01(cursorSmallerWithDistance);
    }
}
