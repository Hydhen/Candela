using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(Camera))]
public class ThirdPersonCamera : MonoBehaviour {

    [Tooltip("Target")]
    public GameObject Target = null;
    [Tooltip("Rotation Speed")]
    public float RotationSpeed = 5f;
    //Rewired
    public int PlayerId = 0;


    private Camera Cam = null;
    private Vector3 Offset = Vector3.zero;
    private float ShoulderOffset = 0;
    private bool LeftShoulder = true;
    //Rewired
    private Player Player = null;


	private void Awake()
    {
        Cam = GetComponent<Camera>();
        Player = ReInput.players.GetPlayer(PlayerId);
	}

    private void Start()
    {
        Offset = Target.transform.position - transform.position;
        ShoulderOffset = Offset.x;
    }

    private void UpdatePosition()
    {
        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = Target.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * RotationSpeed);

        Quaternion rotation = Quaternion.Euler(0, angle, 0);

        transform.position = Vector3.Lerp(transform.position,
            Target.transform.position - (rotation * Offset) + (!LeftShoulder ? Target.transform.right * (ShoulderOffset * 2) : Vector3.zero),
            Time.deltaTime * RotationSpeed);
    }

    private void UpdateLook()
    {
        Quaternion rotation = Target.transform.rotation;
        transform.rotation = rotation;
        //transform.LookAt(Target.transform);
    }

    private void Update()
    {
        if (Player.GetButtonDown("Shoulder"))
        {
            LeftShoulder = !LeftShoulder;
        }
    }

    private void FixedUpdate()
    {
        UpdatePosition();
        UpdateLook();
	}
}
