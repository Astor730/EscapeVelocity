using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    Transform playerBody;
    public float sensitivity = 700;
    float pitch;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent.transform != null)
        {
            playerBody = transform.parent.transform;
        }
        

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

            if(!LevelManager.isGameOver)
            {
                if (transform.parent.transform != null)
                {
                    float moveX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
                    float moveY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

                    //yaw
                    playerBody.Rotate(Vector3.up * moveX);

                    //pitch
                    pitch -= moveY;

                    pitch = Mathf.Clamp(pitch, -90f, 90f);
                    transform.localRotation = Quaternion.Euler(pitch, 0, 0);
                }
            }
            
        
    }
}
