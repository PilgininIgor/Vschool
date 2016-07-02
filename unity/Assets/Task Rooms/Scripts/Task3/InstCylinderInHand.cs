using UnityEngine;
using System.Collections;

public class InstCylinderInHand : MonoBehaviour {

    GameObject currentCylinder;
    int currentCylinderType = 0;
    public GameObject[] cylinders;

    public void GenerateCylinder(int cylinderType)
    {
        Destroy(currentCylinder);
        currentCylinder = Instantiate(cylinders[cylinderType-1], transform.position, transform.rotation) as GameObject;
        currentCylinder.transform.parent = transform;
        currentCylinder.transform.Rotate(new Vector3(0, 180, 0));
        currentCylinder.collider.isTrigger = true;
        currentCylinderType = cylinderType;
    }

    public int CurrentCylinderType()
    {
        return currentCylinderType;
    }

    public void RemoveCurrentCylinder()
    {
        Destroy(currentCylinder);
        currentCylinderType = 0;
    }

}
