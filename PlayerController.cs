using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public Interactable focus;


    private CharacterController playerCC;
    [SerializeField] Vector3 playerVelocity;
    private float playerSpeed = 10.0f;
    private float rotateSpeed = 7f;
    private float jumpHeight = 10.0f;
    private float gravity = -10f;
    [SerializeField] Camera mainCamera;
    [SerializeField] private bool isGrounded;
    private bool hasJumped;

    private void Start()
    {
        playerCC = gameObject.GetComponent<CharacterController>();
        
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 moveGravity = new Vector3(0, gravity, 0);
        playerCC.Move(moveGravity * Time.deltaTime);
        if (verticalInput > 0)
        {
            transform.position += transform.forward * Time.deltaTime * playerSpeed;
        }
        if (verticalInput < 0)
        {
            transform.position -= transform.forward * Time.deltaTime * playerSpeed;
        }
        if (horizontalInput > 0)
        {
            transform.position += transform.right * Time.deltaTime;
            transform.Rotate(new Vector3(0, rotateSpeed, 0), Space.Self);
        }
        if (horizontalInput < 0)
        {
            transform.position -= transform.right * Time.deltaTime;
            transform.Rotate(new Vector3(0, -rotateSpeed, 0), Space.Self);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasJumped = true;
            move.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            playerCC.Move(move * Time.deltaTime);
            playerVelocity.y = move.y;
            }
        if (!playerCC.isGrounded)
        {
            playerVelocity.y += gravity * Time.deltaTime;
            playerCC.Move(playerVelocity * Time.deltaTime);
            if (verticalInput > 0)
            {
                transform.position += transform.forward * Time.deltaTime * playerSpeed;
                if (playerVelocity.y > 0)
                {
                    playerVelocity.y -= transform.forward.y * Time.deltaTime * playerSpeed;
                } else if (playerVelocity.y < 0)
                {
                    playerVelocity.y += transform.forward.y * Time.deltaTime * playerSpeed;
                }
            }
            if (verticalInput < 0)
            {
                transform.position -= transform.forward * Time.deltaTime * playerSpeed;
                if (playerVelocity.y > 0)
                {
                    playerVelocity.y -= transform.forward.y * Time.deltaTime * playerSpeed;
                }
                else if (playerVelocity.y < 0)
                {
                    playerVelocity.y += transform.forward.y * Time.deltaTime * playerSpeed;
                }
            }
            if (horizontalInput > 0)
            {
                transform.position += transform.right * Time.deltaTime;
                transform.Rotate(new Vector3(0, rotateSpeed, 0), Space.World);
            }
            if (horizontalInput < 0)
            {
                transform.position -= transform.right * Time.deltaTime;
                transform.Rotate(new Vector3(0, -rotateSpeed, 0), Space.Self);
            }
        }
        //on mouse click, ray checks what object was clicked and sets that object as focus point
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
               Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
                else if (interactable == null)
                {
                    RemoveFocus();
                }
            }
        }
        if (playerCC.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            
        }
    }
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {   if (focus != null)
            {
                focus.OnDefocus();
            }
            focus = newFocus;

        }
        newFocus.OnFocused(transform);
    }
    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocus();

        }
        focus = null;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
            hasJumped = false;
        }
    }
   /* private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            playerVelocity.y = gravity;

        }
    }*/

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
        if (collision.gameObject.layer == 7)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Destroy(collision.gameObject);
            }
        }
            
        
    }
}