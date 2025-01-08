using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : VCNVMonoBehaviour
{
    [SerializeField] protected JunkCtrl junkCtrl;
    public JunkCtrl JunkCtrl => junkCtrl;
    [SerializeField] protected Animator _animator;
    public Animator Animator => _animator;
    [SerializeField] protected Score score;
    public Score Score => score;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadJunkCtrl();
        this.LoadAnimator();
        this.LoadScore();
    }

    protected virtual void LoadScore()
    {
        if (this.score != null) return;
        this.score = GameObject.FindObjectOfType<Score>();
        Debug.LogWarning(transform.name + ": LoadScore", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this._animator != null) return;
        this._animator = transform.GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadJunkCtrl()
    {
        if (this.junkCtrl != null) return;
        this.junkCtrl = transform.parent.parent.parent.GetComponent<JunkCtrl>();
        Debug.LogWarning(transform.name + ": LoadJunkCtrl", gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Đã nhặt");
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetTrigger("Hit");
            this.junkCtrl.Spawner.Despawn(transform);
            this.score.PickupItem();
        }
    }
}
