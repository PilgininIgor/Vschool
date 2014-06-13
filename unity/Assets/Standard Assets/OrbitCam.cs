using UnityEngine;
using System.Collections;

public class OrbitCam : MonoBehaviour
{
	public Transform target;
    public GameObject player;
	public Vector3 targetOffset = Vector3.zero;
	public Vector3 movementDirection = Vector3.zero;
	public float distance = 4.0f;
	
	public LayerMask lineOfSightMask;
	public float closerRadius = 0.2f;
	public float closerSnapLag = 0.2f;
	
	public float xSpeed = 200.0f;
	public float ySpeed = 80.0f;
	
	public float yMinLimit = -20;
	public float yMaxLimit = 80;
	public Touch touch;
	
	float currentDistance = 10.0f;
	public float x = 0.0f;
	public float y = 0.0f;
	private float distanceVelocity = 0.0f;
	
	public bool handleInputFromMouse = true;
	
	void Start () 
	{
		if (InterfaceIdiom.Current != InterfaceIdiom.Values.Desktop)
			handleInputFromMouse = false;
		
	    var angles = transform.eulerAngles;
	    x = angles.y;
	    y = angles.x;
		currentDistance = distance;
		
		// Make the rigid body not change rotation
	   	if (rigidbody)
			rigidbody.freezeRotation = true;
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void LateUpdate () 
	{
	    if (target) 
	    {
	    	if (handleInputFromMouse)
	    	{
		    	if (Input.GetMouseButton(1)) 
		    	{
		    		//добавили поворот камеры только по нажатию мышки
		    		//0 - левая, 1 - правая, 2 - средняя кнопки мыши
		    		Rotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		        	//x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
		        	//y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;
		        }
	        }
			
			if (movementDirection != Vector3.zero)
			{
				float movementAngle = (Mathf.Atan2(movementDirection.x, movementDirection.z) / Mathf.PI) * 180f;
				if (movementAngle - x > 180)
				{
					movementAngle -= 360;
				}
				if (x - movementAngle > 180)
				{
					movementAngle += 360;
				}
				
				x = Mathf.Lerp(x, movementAngle, 0.4f*Time.deltaTime);
				y = Mathf.Lerp(y, 30, 0.4f*Time.deltaTime);
			}
	 		       
	        var rotation = Quaternion.Euler(y, x, 0);
	        var targetPos = target.position + targetOffset;
	        var direction = rotation * -Vector3.forward;
			
	        var targetDistance = AdjustLineOfSight(targetPos, direction);
			currentDistance = Mathf.SmoothDamp(currentDistance, targetDistance, ref distanceVelocity, closerSnapLag * .3f);
			
	        transform.rotation = rotation;
	        transform.position = targetPos + direction * currentDistance;
			
			y = ClampAngle (y, yMinLimit, yMaxLimit);
	    }
	}
	       
	public void Rotate (float x1, float y1)
	{
		x += x1 * xSpeed * 0.02f;
		y -= y1 * ySpeed * 0.02f;      
	}


	float AdjustLineOfSight (Vector3 target, Vector3 direction)
	{
		RaycastHit hit;
		if (Physics.Raycast (target, direction, out hit, distance, lineOfSightMask.value))
			return hit.distance - closerRadius;
		else
			return distance;
	}
	
	static float ClampAngle (float angle,float min,float max) 
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}

