using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : VCNVMonoBehaviour
{
    [SerializeField] protected Button btnLeft;
    [SerializeField] protected Button btnRight;
    [SerializeField] protected Button btnJump;
    [SerializeField] protected PlayerCtrl playerCtrl;
    private static TouchController instance;
    public static TouchController Instance => instance;
    public bool isMovingLeft = false;
    public bool isMovingRight = false;
    public bool isJumping = false;
    public bool hasJumped = false;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.Log("Just only 1 TouchController allow exists");
        instance = this;
    }

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadBtnLeft();
        this.LoadBtnRight();
        this.LoadBtnJump();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GameObject.FindObjectOfType<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl");
    }

    protected virtual void LoadBtnLeft()
    {
        if (this.btnLeft != null) return;
        this.btnLeft = transform.Find("ButtonLeft").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnLeft");
    }

    protected virtual void LoadBtnRight()
    {
        if (this.btnRight != null) return;
        this.btnRight = transform.Find("ButtonRight").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnRight");
    }

    protected virtual void LoadBtnJump()
    {
        if (this.btnJump != null) return;
        this.btnJump = transform.Find("ButtonJump").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnJump");
    }

    protected override void Update()
    {
        base.Update();
        this.PlayerMoveByMobile();
    }

    public void JumpOnClick()
    {
        int jumCount = this.playerCtrl.Movement.JumCount;
        int maxJumpCount = this.playerCtrl.Movement.MaxJumCount;
        Animator animator = this.playerCtrl.Animator;
		if (jumCount < maxJumpCount)
		{
			Jump();
            jumCount++;
			this.playerCtrl.Movement.SetJumpCount(jumCount);

			if (jumCount == 1) 
			{
				animator.SetBool("isJump", true);
			}
			else if (jumCount == 2) 
			{
				animator.SetBool("isDoubleJump", true);
			}
		}

		Debug.Log("nhảy lên");
    }

    public virtual void Jump()
    {
        float jumpForce = this.playerCtrl.Movement.JumpForce;
        Rigidbody2D rb = this.playerCtrl.Rigidbody2D;

        rb.velocity = new Vector2(rb.velocity.x, 0f);

		this.playerCtrl.Rigidbody2D.AddForce(Vector2.up * (jumpForce+2), ForceMode2D.Impulse);
	}


    public void StartMovingLeft()
    {
        this.isMovingLeft = true;
    }

    public void StopMovingLeft()
    {
        this.isMovingLeft=false;
    }
    public void StartMovingRight()
    {
        this.isMovingRight = true;
    }

    public void StopMovingRight()
    {
        this.isMovingRight = false;
    }

    protected virtual void PlayerMoveByMobile()
    {
        if(this.isMovingLeft)
        {
            this.MoveLeft();
        }else if (this.isMovingRight)
        {
            this.MoveRight();
        }
    }

    protected void MoveLeft()
    {
        Rigidbody2D rb = this.playerCtrl.Rigidbody2D;
        rb.velocity = new Vector2(-this.playerCtrl.Movement.MoveSpeed, rb.velocity.y);
		this.playerCtrl.transform.localScale = new Vector3(-1f, 1f, 1f);
        this.playerCtrl.Animator.SetBool("isRunning", true);
	}

	protected void MoveRight()
	{
		Rigidbody2D rb = this.playerCtrl.Rigidbody2D;
		rb.velocity = new Vector2(this.playerCtrl.Movement.MoveSpeed, rb.velocity.y);
		this.playerCtrl.transform.localScale = new Vector3(1f, 1f, 1f);
		this.playerCtrl.Animator.SetBool("isRunning", true);
	}
}
