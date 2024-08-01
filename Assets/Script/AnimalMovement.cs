using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField] protected float horizontal;
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float jumpingPower = 10f;

    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected TouchingDirection touchingDirection;

    [SerializeField] protected bool _isSelected = false;
    public bool IsSelected
    {
        get { return _isSelected; }
        set { 
            _isSelected = value;
            animator.SetBool(AnimationString.isSelected, value);

            if (!value)
            {
                horizontal = 0f;
                IsMoving = false;
            }
        }
    }

    [SerializeField] protected bool _isShowTime = false;
    public bool IsShowTime
    {
        get { return _isShowTime; }
        set
        {
            _isShowTime = value;
            animator.SetBool(AnimationString.isShowTime, value);
        }
    }

    [SerializeField] protected bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationString.isMoving, value);
        }
    }

    protected bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                // Flip
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    protected float Speed
    {
        get
        {
            if (!CanMove) return 0;
            if (!_isMoving) return 0;
            if (touchingDirection.IsOnWall) return 0;

            return speed;
        }
    }
    public bool CanMove
    {
        get { return animator.GetBool(AnimationString.canMove); }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
    }

    private void Update()
    {
        if (!IsSelected) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        Moving();
        Jumping();
    }


    private void SetFacingDirection(float horizontal)
    {
        if (horizontal > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (horizontal < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    protected void Moving()
    {
        IsMoving = horizontal != 0;
        SetFacingDirection(horizontal);

        rb.velocity = new Vector2(horizontal * Speed, rb.velocity.y);

        animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
    }

    protected void Jumping()
    {
        if (!touchingDirection.IsGrounded) return;
        if (!CanMove) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(AnimationString.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TargetArea"))
        {
            Debug.Log("Animal enter target area");

            int animalCount = PlayerPrefs.GetInt("animal_in_target_area_count");
            PlayerPrefs.SetInt("animal_in_target_area_count", animalCount + 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TargetArea"))
        {
            Debug.Log("Animal exit target area");

            int animalCount = PlayerPrefs.GetInt("animal_in_target_area_count");
            PlayerPrefs.SetInt("animal_in_target_area_count", animalCount - 1);
        }
    }
}
