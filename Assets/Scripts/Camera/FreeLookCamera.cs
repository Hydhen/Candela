using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FreeLookCamera : MonoBehaviour {

    public float Speed = 5f;

    public float LookSpeed = 5f;


    private void Update()
    {

        #region Movement

        if (Input.GetKey(KeyCode.Z))
        {
            transform.Translate(transform.forward * Speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-transform.forward * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(-transform.right * Speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * Speed * Time.deltaTime);
        }

        #endregion


        #region Look

        float x = Input.GetAxis("Mouse X") * LookSpeed * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * LookSpeed * Time.deltaTime;
        // rotate the character based on the x value
        transform.Rotate(-y, x, 0);

        #endregion

    
        #region Ping

        if (Input.GetKeyDown(KeyCode.W))
        {
            RaycastHit hit;

            Physics.Raycast(transform.position, transform.forward, out hit);

            Debug.Log("Hit at " + hit.distance);

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            cube.transform.localPosition = hit.point;

            cube.layer = LayerMask.NameToLayer("Walls Enabled");
        }

        #endregion
    
    }

}
