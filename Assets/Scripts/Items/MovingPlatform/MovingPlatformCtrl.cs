using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformCtrl : VCNVMonoBehaviour
{
    [SerializeField] protected MovingPlatform_Animation platform_Animation;
    public MovingPlatform_Animation MovingPlatform_Animation=> platform_Animation;
    [SerializeField] protected PlayerCtrl player_Ctrl;
    public PlayerCtrl PlayerCtrl => player_Ctrl;

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadMovingPlatform_Animation();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.player_Ctrl != null) return;
        this.player_Ctrl = GameObject.FindObjectOfType<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void LoadMovingPlatform_Animation()
    {
        if (this.platform_Animation != null) return;
        this.platform_Animation=transform.GetComponentInChildren<MovingPlatform_Animation>();
        Debug.LogWarning(transform.name + ": LoadMovingPlatform_Animation", gameObject);
    }
}
