using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCamera : MonoBehaviour {

    public Transform StartTarget = null;

    public Vector3 PositionOffset = new Vector3(0, 0, 5);

    public float Speed = 5f;


    private Vector3 Target = Vector3.zero;


    public Vector3 GetTarget()
    {
        return Target;
    }

    public void SetTarget(Vector3 position)
    {
        Target = position;
    }

    public void SetTargetOffset(Vector3 offset)
    {
        Target += offset;
    }


    private void Awake()
    {
        if (StartTarget)
        {
            Target = StartTarget.position;
        }
    }

    private void FixedUpdate()
    {
        if (Target != null)
        {
            // Get new position
            Vector3 newPosition = Target + PositionOffset;

            // Lerp to this position
            transform.position = Vector3.Lerp(transform.position, newPosition, Speed * Time.fixedDeltaTime);
        }
    }

}
