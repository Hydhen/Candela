using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Rewired;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{

    #region Public Variables

    //Rewired
    public int PlayerId = 0;

    #endregion


    #region Private Variables

    private ThirdPersonCharacter Character;
    private Transform Cam;
    private Vector3 CamForward;
    private Vector3 Move;
    private bool Jump;
    //Rewired
    private Player Player;

    #endregion


    #region Private Methods

    private void Awake()
    {
        Player = ReInput.players.GetPlayer(PlayerId);
    }

    private void Start()
    {
        if (Camera.main != null)
        {
            Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
        }

        Character = GetComponent<ThirdPersonCharacter>();
    }

    private void Update()
    {
        if (!Jump)
        {
            Jump = Player.GetButton("Jump");
        }
        Debug.Log("Jump : " + Jump);
    }

    private void FixedUpdate()
    {
        float h = Player.GetAxis("MoveHorizontal");
        float v = Player.GetAxis("MoveVertical");
        bool crouch = Player.GetButton("Crouch");

        Debug.Log("Crouch : " + crouch);

        if (Cam != null)
        {
            CamForward = Vector3.Scale(Cam.forward, new Vector3(1, 0, 1)).normalized;
            Move = v * CamForward + h * Cam.right;
        }
        else
        {
            Move = v * Vector3.forward + h * Vector3.right;
        }

        Character.Move(Move, crouch, Jump);
        Jump = false;
    }

    #endregion
}
