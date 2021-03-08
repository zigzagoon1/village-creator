using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 offset2;
    [SerializeField] private Quaternion rotationOffset;

    private Space space = Space.Self;
    float distance;
    Vector3 playerPrevPos, playerMoveDir;
    private void Start()
    {
        transform.LookAt(target);
        offset2 = transform.position - target.transform.position;
        distance = offset2.magnitude;
        playerPrevPos = target.transform.position;
    }

    private void Update()
    {
   
    }
    private void LateUpdate()
    {
        playerMoveDir = target.transform.position - playerPrevPos;
        playerMoveDir = playerMoveDir.normalized;

        if (playerMoveDir != Vector3.zero && playerMoveDir != Vector3.up)
        {
            transform.position += Vector3.up;
            transform.position = Vector3.Slerp(transform.position, target.transform.position - playerMoveDir * distance, 0.1f);
            transform.LookAt(target.transform.position);
            playerPrevPos = target.transform.position;
        }
        
    }
}