using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : VCNVMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl=>playerCtrl;
    [SerializeField] protected int direction;
    [SerializeField] protected bool movePc;

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadPlayerCtrl();
    }

	protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected override void Update()
    {
        base.Update();
        if (movePc) 
        {
            this.HandleRunAnimationPc();
        }
        else
        {
			this.HandleRunAnimation();
		}
	}

	protected virtual void HandleRunAnimationPc()
	{
        float horizontalInput = Input.GetAxis("Horizontal");
		var animator = this.playerCtrl.Animator;
		if (horizontalInput!=0)
		{
			animator.SetBool("isRunning", true);
			if (horizontalInput<0)
			{
				this.direction = -1;
				transform.parent.localScale = new Vector3(-1f, 1f, 1f);
			}
			else if (horizontalInput>0)
			{
				this.direction = 1;
				transform.parent.localScale = new Vector3(1f, 1f, 1f);
			}
		}
		else
		{
			this.direction = 0;
			animator.SetBool("isRunning", false);
		}
	}

	protected virtual void HandleRunAnimation()
    {
        bool isMovingLeft = TouchController.Instance.isMovingLeft;
        bool isMovingRight = TouchController.Instance.isMovingRight;
        var animator = this.playerCtrl.Animator;
        Debug.Log("Result: " + isMovingRight + isMovingLeft);
        if (isMovingRight||isMovingLeft)
        {
            animator.SetBool("isRunning", true);
            if (isMovingLeft)
            {
                this.direction = -1;
                transform.parent.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if(isMovingRight)
            {
                this.direction = 1;
                transform.parent.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            this.direction = 0;
            animator.SetBool("isRunning", false);
        }
    }

}
