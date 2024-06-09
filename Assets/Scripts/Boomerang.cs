using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float throwSpeed = 5f;
    public float returnSpeed = 7f;
    public float flightDuration = 2f;
    public AnimationCurve flightCurve; 

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 controlPoint;
    private float flightTime;
    private bool returning;

    public float FlightTime => flightTime;

    void Start()
    {
        flightTime = 0f;
        returning = false;
    }

    public void Throw(Vector3 target)
    {
        startPosition = transform.position;
        targetPosition = target;
        controlPoint = (startPosition + targetPosition) / 2 + Vector3.up * 14f; // Adjust this for a higher or lower curve
        flightTime = 0.01f; // Start the flight time to indicate it's thrown
        returning = false;
        Debug.Log("Boomerang thrown to: " + targetPosition);
    }

    void Update()
    {
        if (flightTime > 0)
        {
            flightTime += Time.deltaTime;
            float t = flightTime / flightDuration;

            if (!returning)
            {
                if (t >= 1f)
                {
                    returning = true;
                    flightTime = 0.01f; // Reset flight time for the return journey
                    Debug.Log("Boomerang returning");
                }
                else
                {
                    MoveBoomerang(t, startPosition, controlPoint, targetPosition);
                }
            }
            else
            {
                if (t >= 1f)
                {
                    // Reset the boomerang to the hand position
                    flightTime = 0f;
                    returning = false;
                    transform.position = startPosition;
                    Debug.Log("Boomerang returned to start");
                }
                else
                {
                    MoveBoomerang(t, targetPosition, controlPoint, startPosition);
                }
            }

            // Rotate the boomerang
            transform.Rotate(0, 0, 360 * Time.deltaTime);
        }
    }

    private void MoveBoomerang(float t, Vector3 start, Vector3 control, Vector3 end)
    {
        Vector3 m1 = Vector3.Lerp(start, control, t);
        Vector3 m2 = Vector3.Lerp(control, end, t);
        transform.position = Vector3.Lerp(m1, m2, t);
    }
}
