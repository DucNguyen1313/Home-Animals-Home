using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    protected float groundDistance = 0.05f;
    protected float wallDistance = 0.05f;

    [SerializeField] protected ContactFilter2D castFilterForGround;
    [SerializeField] protected ContactFilter2D castFilterForWall;

    [SerializeField] protected BoxCollider2D touchingCollider;
    [SerializeField] protected RaycastHit2D[] groundHits = new RaycastHit2D[5];
    [SerializeField] protected RaycastHit2D[] wallHits = new RaycastHit2D[5];
    [SerializeField] protected Animator animator;

    protected Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    [SerializeField] protected bool _isGrounded = true;
    public bool IsGrounded
    {
        get { return _isGrounded; }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationString.isGrounded, value);
        }
    }

    [SerializeField] protected bool _isOnWall = true;
    public bool IsOnWall
    {
        get { return _isOnWall; }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationString.isOnWall, value);
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        touchingCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = touchingCollider.Cast(Vector2.down, castFilterForGround, groundHits, groundDistance) > 0;
        IsOnWall = touchingCollider.Cast(wallCheckDirection, castFilterForWall, wallHits, wallDistance) > 0;
    }
}
