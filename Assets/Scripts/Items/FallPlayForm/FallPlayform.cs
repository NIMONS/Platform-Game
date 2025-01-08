using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlayform : VCNVMonoBehaviour
{
    [SerializeField] protected Animator _animator;
    public Animator Animator => _animator;

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadAnimatior();
    }

    protected virtual void LoadAnimatior()
    {
        if (this._animator != null) return;
        this._animator=transform.GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimatior", gameObject);
    }

    
}
