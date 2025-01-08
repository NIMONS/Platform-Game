using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadCtrl : VCNVMonoBehaviour
{
    [SerializeField] protected JumpPad_Animation jumpPad_Animation;
    public JumpPad_Animation JumpPad_Animation => jumpPad_Animation;
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadJumpPad_Animation();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GameObject.FindObjectOfType<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void LoadJumpPad_Animation()
    {
        if (this.jumpPad_Animation != null) return;
        this.jumpPad_Animation=transform.GetComponentInChildren<JumpPad_Animation>();
        Debug.LogWarning(transform.name + ": LoadJumpPad_Animation", gameObject);
    }
}
