using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected float upDiff = 2f;
    [SerializeField] protected float rightDiff = 0f;
    protected Vector3 targetPoint;

    [SerializeField] protected bool _isPistonOn = false;
    public bool IsPistonOn
    {
        get { return _isPistonOn; }
        set
        {
            _isPistonOn = value;
        }
    }

    private void Awake()
    {
        targetPoint = new Vector3(transform.position.x + rightDiff, transform.position.y + upDiff, transform.position.z);
    }

    private void Update()
    {
        if (IsPistonOn)
        {
            PushUp();
        }
    }
    protected void PushUp()
    {
        if (Vector2.Distance(transform.position, targetPoint) > 0.1f)
        {
            Vector2 direction = (targetPoint - transform.position).normalized;

            transform.position += (Vector3) direction * moveSpeed * Time.deltaTime;
        }
    }
}
