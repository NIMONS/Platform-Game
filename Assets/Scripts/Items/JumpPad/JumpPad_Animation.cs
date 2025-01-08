using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad_Animation : VCNVMonoBehaviour
{
    [SerializeField] protected JumpPadCtrl jumpPadCtrl;
    public JumpPadCtrl JumpPadCtrl=> jumpPadCtrl;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;
    [SerializeField] protected float jumpForce = 20f;

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadJumpPadCtrl();
        this.LoadAnimator();
    }
    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = transform.GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadJumpPadCtrl()
    {
        if (this.jumpPadCtrl != null) return;
        this.jumpPadCtrl = transform.parent.GetComponent<JumpPadCtrl>();
        Debug.LogWarning(transform.name + ": LoadJumpPadCtrl", gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (animator != null) animator.SetBool("Hit", true);
            this.jumpPadCtrl.PlayerCtrl.Rigidbody2D.velocity = Vector2.up * this.jumpForce;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (animator != null) animator.SetBool("Hit", false);

        }
    }
}
