using UnityEngine;
using System.Collections;

public class EndOfLevel : MonoBehaviour
{

    public float timeToTriggerLevelEnd = 2.0f;
    public string endSceneName = "3-4_Pain";

    IEnumerator Wait(float s)
    {
        yield return new WaitForSeconds(s);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            FadeOutAudio();

            var playerMove = other.gameObject.GetComponent<PlayerMoveController>();
            playerMove.enabled = false;

            float timeWaited = 0.0f;
            var playerMotor = other.gameObject.GetComponent<FreeMovementMotor>();
            while (playerMotor.walkingSpeed > 0.0f)
            {
                playerMotor.walkingSpeed -= Time.deltaTime * 6.0f;
                if (playerMotor.walkingSpeed < 0.0f)
                    playerMotor.walkingSpeed = 0.0f;
                timeWaited += Time.deltaTime;
            }
            playerMotor.walkingSpeed = 0.0f;

            StartCoroutine(Wait(Mathf.Clamp(timeToTriggerLevelEnd - timeWaited, 0.0f, timeToTriggerLevelEnd)));

            Camera.main.gameObject.SendMessage("WhiteOut");
            StartCoroutine(Wait(2));

            Application.LoadLevel(endSceneName);
        }
    }

    void FadeOutAudio()
    {
        var al = Camera.main.gameObject.GetComponent<AudioListener>();
        if (al)
        {
            while (AudioListener.volume > 0.0f)
            {
                AudioListener.volume -= Time.deltaTime / timeToTriggerLevelEnd;
            }
        }
    }
}
