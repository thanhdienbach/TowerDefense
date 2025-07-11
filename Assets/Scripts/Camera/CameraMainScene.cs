using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMainScene : MonoBehaviour
{

    public Transform mainViewTransform;
    public Transform turretLookTransform;
    public float moveTime = 0.5f;


    public void SmothMoveToTurret()
    {
        StartCoroutine(SmothMoveToPoint(turretLookTransform, moveTime));
    }
    public void SmothMoveToMainView()
    {
        StartCoroutine(SmothMoveToPoint(mainViewTransform, moveTime));
    }
    IEnumerator SmothMoveToPoint(Transform destination, float moveDuration)
    {
        Vector3 startPoint = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPoint, destination.position, elapsedTime/moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = destination.position;
    }
}
