using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected List <Piston> pistons;

    protected bool firstCheck = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animator.SetBool(AnimationString.isClick, false);

        firstCheck = true;
    }

    private void Update()
    {
        if (animator.GetBool(AnimationString.isClick) && firstCheck)
        {
            Debug.Log("wow");
            firstCheck = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Block") return;

        if (animator.GetBool(AnimationString.isClick)) return;
        
        Debug.Log("Player click button");
        animator.SetBool(AnimationString.isClick, true);

        foreach (Piston piston in pistons) {
            piston.IsPistonOn = true;
        }
    }
}
