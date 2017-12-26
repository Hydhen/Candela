using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;


public class PlayerCamera : MonoBehaviour
{

    public GameObject Anchor = null;
    public float Radius = 10f;
    public float RotationSpeed = 5f;
    public float LookOffset = -5f;


    private Vector3 Offset = Vector3.zero;
    // ReWired
    private Player Player = null;

    private void Awake()
    {
        if (Anchor == null)
        {
            Debug.LogError("<color='Red'>No Anchor given</color>", this);
        }

        Player = ReInput.players.GetPlayer(0);
    }

    private void Start()
    {
        Offset = transform.position - Anchor.transform.position;
    }

    private void Update()
    {
        if (Player.GetButtonDown("Shoulder"))
        {
            LookOffset *= -1;
        }
    }

    private void LateUpdate()
    {
        Offset = Quaternion.AngleAxis(Player.GetAxis("LookHorizontal") * RotationSpeed, Vector3.up) * Offset;
        transform.position = Anchor.transform.position + Offset;
        transform.LookAt(Anchor.transform.position);
        Debug.Log(transform.rotation.y);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + LookOffset, 0);
    }

}