using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    /* The transform component of the target
    actor that is being followed by the camera. */
    public Transform target;

    /* How much is the player allowed to move in the x
    axis before the camera starts following him. */
    public float xMaxTargetDeviation = 0.5f;

    /* How much is the player allowed to move in the y
    axis before the camera starts following him. */
    public float yMaxTargetDeviation = 0.3f;

    private void LateUpdate()
    {
        Vector3 cameraTranslate = Vector3.zero;

        float xDeviation = Mathf.Abs(target.position.x - transform.position.x);
        float yDeviaton = Mathf.Abs(target.position.y - transform.position.y);

        if (xDeviation > xMaxTargetDeviation)
        {
            cameraTranslate.x = xDeviation - xMaxTargetDeviation;

            if (target.position.x < transform.position.x)
            {
                cameraTranslate.x = - cameraTranslate.x;
            }
        }

        if (yDeviaton > yMaxTargetDeviation)
        {
            cameraTranslate.y = yDeviaton - yMaxTargetDeviation;

            if (target.position.y < transform.position.y)
            {
                cameraTranslate.y = - cameraTranslate.y;
            }
        }

        transform.position += cameraTranslate;
    }
}
