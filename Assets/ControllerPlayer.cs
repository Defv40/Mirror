using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class ControllerPlayer : NetworkBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField][Range(0, 20)] private float speed;
    private float _input;
    [SerializeField][Range(0, 1000)] private float jumpForce;

    private CinemachineVirtualCamera _cam;
    [SerializeField] private Animator animator;
    private void Start()
    {
        if (!isLocalPlayer) return;
        _cam =  GameObject.FindAnyObjectByType<CinemachineVirtualCamera>();
        _cam.Follow = transform;
        _cam.LookAt = transform;
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        _input = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        
        //_rb.MovePosition(_rb.position + _input * speed / 100);
        Vector2 movementDirection = new Vector2(_input, 0);
        movementDirection *= speed;
        if (movementDirection.x != 0)
        {
           
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (movementDirection.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        else if(movementDirection.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        movementDirection.y = _rb.velocity.y;

        
        _rb.velocity = movementDirection;
    }

    private void Jump()
    {
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
