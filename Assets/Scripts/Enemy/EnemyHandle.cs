using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHandle : Moving
{
    [Header("Enemy Handle")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer=>spriteRenderer;
    [SerializeField] protected Animator _animator;
    public Animator Animator => _animator;
    [SerializeField] protected PlayerCtrl _playerCtrl;
    [SerializeField] protected int damageAmount = 1;

    [SerializeField] protected bool isAlive=true;
    public bool IsAlive=>isAlive;

    protected override void Update()
    {
        base.Update();
        this.EnemyMoving();
    }

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadSpriteRenderer();
        this.LoadAnimator();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this._playerCtrl != null) return;
        this._playerCtrl = GameObject.FindObjectOfType<PlayerCtrl>();
        //Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this._animator != null) return;
        this._animator = transform.GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (this.spriteRenderer != null) return;
        this.spriteRenderer=transform.GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadSpriteRenderer", gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&this.isAlive)
        {
            this.HanldeTouchEnemy();
        }
    }

    protected virtual void HanldeTouchEnemy()
    {
        this._playerCtrl.PlayerHealth.TakeDame(this.damageAmount);
    }

    public virtual void HandleEnemyDie(bool value)
    {
        //value is fasle
        if (value==false)
        {
            Invoke("DestroyObj", 0.4f);
            this.HandleEnemyDieAnimation();
        }
    }

    protected virtual void DestroyObj()
    {
        Destroy(transform.parent.gameObject);
    }

    protected virtual void EnemyMoving()
    {
        Vector2 posA = this.PosA.transform.position;
        Vector2 posB = this.PosB.transform.position;
        Vector2 currentPos = transform.parent.position;

        transform.parent.position = Vector2.Lerp(posA, posB, Mathf.PingPong(this.moveSpeed * Time.time,1));

        float PosA =Vector2.Distance(posA, currentPos);
        float PosB=Vector2.Distance(posB, currentPos);
        if (this.isMoveToB)
        {
            transform.parent.localScale = new Vector3(1, 1, 1);
            if (Vector2.Distance(currentPos, posA) < 0.01f)
            {
                this.isMoveToB = false;
            }
        }
        else
        {
            transform.parent.localScale = new Vector3(-1, 1, 1);
            if (Vector2.Distance(currentPos, posB) < 0.01f)
            {
                this.isMoveToB = true;
            }
        }
    }

    protected virtual void HandleEnemyDieAnimation()
    {
        if(this.Animator != null)
        {
            this._animator.SetTrigger("Hit");
        }
    }
}
