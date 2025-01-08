using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : VCNVMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl=>playerCtrl;

    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 10f;
     public float JumpForce => jumpForce;
    [SerializeField] protected int jumCount = 0;
    public int JumCount => jumCount;
    [SerializeField] protected int maxJumCount = 2;
    public int MaxJumCount => maxJumCount;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadPlayerCtrl();
    }

    protected override void Update()
    {
        this.MovePlayer();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void MovePlayer()
    {
        Animator animator = this.playerCtrl.Animator;
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement =Vector2.zero;
        
        bool isMovingLeft= TouchController.Instance.isMovingLeft;
        bool isMovingRight = TouchController.Instance.isMovingRight;
        bool isJumping = TouchController.Instance.isJumping;
        bool hasJumped = TouchController.Instance.hasJumped;

        if(isMovingLeft)
        {
            movement += Vector2.left * this.moveSpeed * Time.deltaTime;
        }else if(isMovingRight)
        {
            movement += Vector2.right * this.moveSpeed * Time.deltaTime;
        }
        transform.parent.Translate(movement);
        transform.parent.rotation = Quaternion.identity;

        if (isJumping)
        {
            if (this.jumCount < this.maxJumCount)
            {
                animator.SetBool("isJump", true);
                this.Jump();
                this.jumCount++;
            }
            else
            {
                TouchController.Instance.isJumping=false;
            }
        }
    }

    public int GetJumpCount()
    {
        return this.jumCount;
    }

    public virtual void Jump()
    {
        //Debug.Log("Jumping");
        this.playerCtrl.Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public virtual void Grounded()
    {
        Animator animator = this.playerCtrl.Animator;
        animator.SetBool("isRunning", true);
        this.jumCount = 0;
        TouchController.Instance.EndJump();
        TouchController.Instance.jumpCount = 1;
    }

}
