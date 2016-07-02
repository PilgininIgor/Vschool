using UnityEngine;
using System.Collections;

public class TakingCylinder : MonoBehaviour {

    RaycastHit hit;
    public GUIText textHints;
    public GameObject panelLeft;
    public GameObject panelCenter;
    public GameObject panelRight;
    public GameObject colliderLeft;
    public GameObject colliderCenter;
    public GameObject colliderRight;
    public GameObject buttonCheck;
    public GameObject[] cylinders;
    string takenFrom;
    int numberOfTurns = 0;
    public TextMesh taskText4;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 4) && !buttonCheck.GetComponent<CheckResult3>().TaskSolved())
        {
            int cylinderInHand = GameObject.Find("hand").GetComponent<InstCylinderInHand>().CurrentCylinderType();

            if (hit.collider.gameObject == colliderLeft)
            {
                int highestLeft = GameObject.Find("TriggerStart").GetComponent<Game3_Start>().highestLeft;
                if (cylinderInHand > highestLeft && cylinderInHand > 0)
                {
                    textHints.SendMessage("ShowHint", "Нажмите E");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GameObject[] cyls = GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersLeft;
                        int height = 0;
                        for (int i = 0; i < cyls.Length; i++)
                        {
                            if (cyls[i] != null) height++;
                        }

                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersLeft[cylinderInHand - 1] = Instantiate(cylinders[cylinderInHand - 1], panelLeft.transform.position, panelLeft.transform.rotation) as GameObject;
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersLeft[cylinderInHand - 1].name = "cyl" + cylinderInHand;
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersLeft[cylinderInHand - 1].transform.parent = panelLeft.transform;
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersLeft[cylinderInHand - 1].transform.localPosition = new Vector3(0, 1.2f * (height + 1), 0);
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersLeft[cylinderInHand - 1].transform.Rotate(new Vector3(0, 180, 0));

                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().highestLeft = cylinderInHand;

                        GameObject.Find("hand").GetComponent<InstCylinderInHand>().RemoveCurrentCylinder();

                        if (takenFrom != "left")
                        {
                            numberOfTurns++;
                            taskText4.text = "Количество ходов: " + numberOfTurns;
                        }
                    }
                }
                if (cylinderInHand == 0 && highestLeft > 0)
                {
                    textHints.SendMessage("ShowHint", "Нажмите E");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GameObject.Find("hand").GetComponent<InstCylinderInHand>().GenerateCylinder(highestLeft);
                        takenFrom = "left";

                        Destroy(GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersLeft[highestLeft - 1]);
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersLeft[highestLeft - 1] = null;

                        GameObject[] cyls = GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersLeft;
                        int last = 0;
                        for (int i = 0; i < cyls.Length; i++)
                        {
                            if (cyls[i] != null) last = i + 1;
                        }
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().highestLeft = last;                            
                    }
                }                
            }
            else
            {
                textHints.SendMessage("ShowHint", "");
            }
            if (hit.collider.gameObject == colliderCenter)
            {
                int highestCenter = GameObject.Find("TriggerStart").GetComponent<Game3_Start>().highestCenter;
                if (cylinderInHand > highestCenter && cylinderInHand > 0)
                {
                    textHints.SendMessage("ShowHint", "Нажмите E");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GameObject[] cyls = GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersCenter;
                        int height = 0;
                        for (int i = 0; i < cyls.Length; i++)
                        {
                            if (cyls[i] != null) height++;
                        }

                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersCenter[cylinderInHand - 1] = Instantiate(cylinders[cylinderInHand - 1], panelCenter.transform.position, panelCenter.transform.rotation) as GameObject;
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersCenter[cylinderInHand - 1].name = "cyl" + cylinderInHand;
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersCenter[cylinderInHand - 1].transform.parent = panelCenter.transform;
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersCenter[cylinderInHand - 1].transform.localPosition = new Vector3(0, 1.2f * (height + 1), 0);
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersCenter[cylinderInHand - 1].transform.Rotate(new Vector3(0, 180, 0));

                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().highestCenter = cylinderInHand;

                        GameObject.Find("hand").GetComponent<InstCylinderInHand>().RemoveCurrentCylinder();

                        if (takenFrom != "center")
                        {
                            numberOfTurns++;
                            taskText4.text = "Количество ходов: " + numberOfTurns;
                        }
                    }
                }
                if (cylinderInHand == 0 && highestCenter > 0)
                {
                    textHints.SendMessage("ShowHint", "Нажмите E");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GameObject.Find("hand").GetComponent<InstCylinderInHand>().GenerateCylinder(highestCenter);
                        takenFrom = "center";

                        Destroy(GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersCenter[highestCenter - 1]);
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersCenter[highestCenter - 1] = null;

                        GameObject[] cyls = GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersCenter;
                        int last = 0;
                        for (int i = 0; i < cyls.Length; i++)
                        {
                            if (cyls[i] != null) last = i + 1;
                        }
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().highestCenter = last;    
                    }
                }
            }
            if (hit.collider.gameObject == colliderRight)
            {
                int highestRight = GameObject.Find("TriggerStart").GetComponent<Game3_Start>().highestRight;
                if (cylinderInHand > highestRight && cylinderInHand > 0)
                {
                    textHints.SendMessage("ShowHint", "Нажмите E");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GameObject[] cyls = GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersRight;
                        int height = 0;
                        for (int i = 0; i < cyls.Length; i++)
                        {
                            if (cyls[i] != null) height++;
                        }

                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersRight[cylinderInHand - 1] = Instantiate(cylinders[cylinderInHand - 1], panelRight.transform.position, panelRight.transform.rotation) as GameObject;
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersRight[cylinderInHand - 1].name = "cyl" + cylinderInHand;
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersRight[cylinderInHand - 1].transform.parent = panelRight.transform;
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersRight[cylinderInHand - 1].transform.localPosition = new Vector3(0, 1.2f * (height + 1), 0);
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersRight[cylinderInHand - 1].transform.Rotate(new Vector3(0, 180, 0));

                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().highestRight = cylinderInHand;

                        GameObject.Find("hand").GetComponent<InstCylinderInHand>().RemoveCurrentCylinder();

                        if (takenFrom != "right")
                        {
                            numberOfTurns++;
                            taskText4.text = "Количество ходов: " + numberOfTurns;
                        }
                    }
                }
                if (cylinderInHand == 0 && highestRight > 0)
                {
                    textHints.SendMessage("ShowHint", "Нажмите E");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GameObject.Find("hand").GetComponent<InstCylinderInHand>().GenerateCylinder(highestRight);
                        takenFrom = "right";

                        Destroy(GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersRight[highestRight - 1]);
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersRight[highestRight - 1] = null;

                        GameObject[] cyls = GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersRight;
                        int last = 0;
                        for (int i = 0; i < cyls.Length; i++)
                        {
                            if (cyls[i] != null) last = i + 1;
                        }
                        GameObject.Find("TriggerStart").GetComponent<Game3_Start>().highestRight = last;    
                    }
                }
            }
            if (hit.collider.gameObject == buttonCheck)
            {
                textHints.SendMessage("ShowHint", "Нажмите E");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    buttonCheck.GetComponent<CheckResult3>().Check();
                }
            }
        }
        else
        {
            textHints.SendMessage("ShowHint", "");
        }
    }

    public int GetNumberOfTurns()
    {
        return numberOfTurns;
    }
}
