using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    Vector3 input;
    Vector3 moveDirection;
    public float speed = 5;

    bool gravityFlipped;

    public float gravity = 9.81f;
    public float jump = 2f;
    public float airControl = 2f;

    float distToGround = 1f;
    bool touchingGround;
    float rotationSpeed = 0.0001f;

    bool hasFlipped;

    public AudioClip flipSFX;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gravityFlipped = false;
        hasFlipped = true;
    }

    // Update is called once per frame
    void Update()
    {

        //if(!hasFlipped)
        //{
        //    Flip();
        //}

        TouchingGround();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        input *= speed;


        if (touchingGround)
        {
            moveDirection = input;
            if (Input.GetButton("Jump"))
            {
                if(!gravityFlipped)
                {
                    moveDirection.y = Mathf.Sqrt(2 * jump * gravity);
                }
                else
                {
                    moveDirection.y = -(Mathf.Sqrt(2 * jump * -gravity));
                }
                
            }
            else
            {
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            Flip();
            //hasFlipped = false;
            
            gravityFlipped = !gravityFlipped;
            gravity = -gravity;

            AudioSource.PlayClipAtPoint(flipSFX, Camera.main.transform.position);
        }

        moveDirection.y -= gravity * Time.deltaTime;

        if(!LevelManager.isGameOver)
        {
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    void TouchingGround()
    {
        touchingGround = Physics.Raycast(transform.position, -transform.up, distToGround + 0.1f);
    }

    void Flip()
    {
        //Quaternion rotation = transform.rotation;
        //Quaternion newRotation = Quaternion.Euler(rotation.x, rotation.y + 270, 180);
        //transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 2);

        transform.Rotate(0, 0, 180);

    }

}
