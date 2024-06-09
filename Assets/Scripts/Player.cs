using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Boomerang boomerang;
    public Transform handPosition; // Position where the boomerang is held
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public int lineSegmentCount = 50; // Number of points in the line renderer
    public float curveHeight = 5f; // Height of the curve

    void Update()
    {
        // Update the boomerang's position to the hand's position while holding
        if (boomerang.FlightTime == 0)
        {
            boomerang.transform.position = handPosition.position;

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10f; // Distance from the camera
                Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                boomerang.Throw(targetPosition);
            }
            else
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10f; // Distance from the camera
                Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                DrawPath(targetPosition);
            }
        }
    }

    void DrawPath(Vector3 targetPosition)
    {
        Vector3 startPosition = handPosition.position;
        Vector3 controlPoint = (startPosition + targetPosition) / 2 + Vector3.up * curveHeight; // Adjust this for a higher or lower curve

        lineRenderer.positionCount = lineSegmentCount;
        for (int i = 0; i < lineSegmentCount; i++)
        {
            float t = i / (float)(lineSegmentCount - 1);
            Vector3 point = CalculateBezierPoint(t, startPosition, controlPoint, targetPosition);
            lineRenderer.SetPosition(i, point);
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 start, Vector3 control, Vector3 end)
    {
        Vector3 m1 = Vector3.Lerp(start, control, t);
        Vector3 m2 = Vector3.Lerp(control, end, t);
        return Vector3.Lerp(m1, m2, t);
    }
}
