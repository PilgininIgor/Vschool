using UnityEngine;

public class CP_Begin_Click : MonoBehaviour
{

    Camera MainCam, PanelCam;
    GameObject PlayerAvatar;

    public SignalSender dieSignals;
    bool panel_activated, hint_visible, finished;

    public CP_Begin_Click()
    {
        PanelCam.enabled = false;
        PanelCam.GetComponent<AudioListener>().enabled = false;
    }


    void Update()
    {
        if (!finished)
        {

            if ((!hint_visible) && (!panel_activated) &&
                (Mathf.Abs(PlayerAvatar.transform.position.x - transform.position.x) < 0.5) &&
                (Mathf.Abs(PlayerAvatar.transform.position.y - transform.position.y) < 2) &&
                (Mathf.Abs(PlayerAvatar.transform.position.z - transform.position.z) < 1)
               )
            {
                transform.Find("GUI Hint").gameObject.active = true; hint_visible = true;
            }
            else
                if ((hint_visible) && (!panel_activated) &&
                    ((Mathf.Abs(PlayerAvatar.transform.position.x - transform.position.x) >= 0.5) ||
                    (Mathf.Abs(PlayerAvatar.transform.position.y - transform.position.y) >= 2) ||
                    (Mathf.Abs(PlayerAvatar.transform.position.z - transform.position.z) >= 1))
                   )
                {
                    transform.Find("GUI Hint").gameObject.active = false; hint_visible = false;
                }

            if ((!panel_activated) && (hint_visible) && (Input.GetKeyDown("e")))
            {
                transform.Find("GUI Hint").gameObject.active = false;
                PlayerAvatar.SetActive(false);
                MainCam.enabled = false; MainCam.GetComponent<AudioListener>().enabled = false;
                PanelCam.enabled = true; PanelCam.GetComponent<AudioListener>().enabled = true;

                GetComponent<BoxCollider>().size = Vector3.zero;
                panel_activated = true;
            }

            if (panel_activated)
            {
                if (Input.GetKeyDown("escape"))
                {
                    MainCam.enabled = true; MainCam.GetComponent<AudioListener>().enabled = true;
                    PanelCam.enabled = false; PanelCam.GetComponent<AudioListener>().enabled = false;
                    PlayerAvatar.SetActive(true); PlayerAvatar.animation.Play("idle");

                    GetComponent<BoxCollider>().size = new Vector3(1.9f, 3, 1);
                    panel_activated = false;
                }
            }

        }
    }

    public void Finish()
    {
        MainCam.enabled = true; MainCam.GetComponent<AudioListener>().enabled = true;
        PanelCam.enabled = false; PanelCam.GetComponent<AudioListener>().enabled = false;
        PlayerAvatar.SetActive(true); PlayerAvatar.animation.Play("idle");
        GetComponent<BoxCollider>().size = new Vector3(1.9f, 3, 1);
        panel_activated = false;

        finished = true;
        dieSignals.SendSignals(this);

    }
}
