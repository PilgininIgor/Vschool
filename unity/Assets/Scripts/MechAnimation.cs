using UnityEngine;
using System.Collections;

public class MechAnimation : MonoBehaviour
{

    Rigidbody rigid;
    AnimationClip idle, walk, turnLeft, turnRight;
    SignalSender footstepSignals;

    private Transform tr;
    private float lastFootstepTime = 0, lastAnimTime = 0;

    void OnEnable()
    {
        tr = rigid.transform;

        animation[idle.name].layer = 0;
        animation[idle.name].weight = 1;
        animation[idle.name].enabled = true;

        animation[walk.name].layer = 1;
        animation[turnLeft.name].layer = 1;
        animation[turnRight.name].layer = 1;

        animation[walk.name].weight = 1;
        animation[turnLeft.name].weight = 0;
        animation[turnRight.name].weight = 0;

        animation[walk.name].enabled = true;
        animation[turnLeft.name].enabled = true;
        animation[turnRight.name].enabled = true;

        //animation.SyncLayer (1);
    }

    void FixedUpdate()
    {
        float turningWeight = Mathf.Abs(rigid.angularVelocity.y) * Mathf.Rad2Deg / 100.0f;
        float forwardWeight = rigid.velocity.magnitude / 2.5f;
        float turningDir = Mathf.Sign(rigid.angularVelocity.y);

        // Temp, until we get the animations fixed
        animation[walk.name].speed = Mathf.Lerp(1.0f, animation[walk.name].length / animation[turnLeft.name].length * 1.33f, turningWeight);
        animation[turnLeft.name].time = animation[walk.name].time;
        animation[turnRight.name].time = animation[walk.name].time;

        animation[turnLeft.name].weight = Mathf.Clamp01(-turningWeight * turningDir);
        animation[turnRight.name].weight = Mathf.Clamp01(turningWeight * turningDir);
        animation[walk.name].weight = Mathf.Clamp01(forwardWeight);

        if (forwardWeight + turningWeight > 0.1)
        {
            var newAnimTime = Mathf.Repeat(animation[walk.name].normalizedTime * 2 + 0.1f, 1);
            if (newAnimTime < lastAnimTime)
            {
                if (Time.time > lastFootstepTime + 0.1)
                {
                    footstepSignals.SendSignals(this);
                    lastFootstepTime = Time.time;
                }
            }
            lastAnimTime = newAnimTime;
        }
    }
}
