using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : VCNVMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl=>playerCtrl;

    [SerializeField] protected float moveSpeed = 9f;
    public float MoveSpeed => moveSpeed;
    [SerializeField] protected float jumpForce = 13f;
     public float JumpForce => jumpForce;
    [SerializeField] protected int jumCount = 0;
    public int JumCount => jumCount;
    [SerializeField] protected int maxJumCount = 2;
    public int MaxJumCount => maxJumCount;
    [SerializeField] protected bool movePc;
    public bool MovePc => movePc;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadPlayerCtrl();
    }

    protected override void Update()
    {
       
        this.MovePlayerPc();
	}

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    public int GetJumpCount()
    {
        return this.jumCount;
    }

    public virtual void Jump()
    {
        Debug.Log("Jumping");
		this.playerCtrl.Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public virtual void Grounded()
    {
        Animator animator = this.playerCtrl.Animator;
        animator.SetBool("isRunning", true);
        this.jumCount = 0;
    }

    protected void MovePlayerPc()
    {
		Animator animator = this.playerCtrl.Animator;
		Rigidbody2D rb = this.playerCtrl.Rigidbody2D;

		float horizontalInput = Input.GetAxisRaw("Horizontal");

		rb.velocity = new Vector2(horizontalInput * this.moveSpeed, rb.velocity.y);

		if (horizontalInput > 0)
			transform.parent.localScale = new Vector3(1f, 1f, 1f);
		else if (horizontalInput < 0)
			transform.parent.localScale = new Vector3(-1f, 1f, 1f);

		animator.SetBool("isRunning", horizontalInput != 0);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (this.jumCount < this.maxJumCount)
			{
				Jump(); 
				this.jumCount++;

				if (jumCount == 1)
				{
					animator.SetBool("isJump", true);
				}
				else if (jumCount == 2) 
				{
					animator.SetBool("isDoubleJump", true);
				}
			}
		}

	}

    public void SetJumpCount(int count)
    {
        this.jumCount = count;
    }
}
