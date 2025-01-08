using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]
public class PlayerCtrl : VCNVMonoBehaviour
{
    [SerializeField] protected Movement movement;
    public Movement Movement => movement;
    [SerializeField] protected Animation _animation;
    public Animation Animation => _animation;
    [SerializeField] protected Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody2D => _rigidbody;
    [SerializeField] protected BoxCollider2D _boxCollider;
    public BoxCollider2D BoxCollider => _boxCollider;
    [SerializeField] protected Animator _animator;
    public Animator Animator => _animator;
    [SerializeField] protected StartPoint startPoint;
    public StartPoint StartPoint => startPoint;
    [SerializeField] protected Platform platform;
    public Platform Platform => platform;
    [SerializeField] protected PlayerHealth health;
    public PlayerHealth PlayerHealth => health;
    [SerializeField] protected BuffHeart heart;
    public BuffHeart Heart => heart;
    [SerializeField] protected Transform _transform;
    public Transform _Transform => _transform;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadMovement();
        this.LoadAnimation();
        this.LoadRigidbody2D();
        this.LoadBoxCollider();
        this.LoadAnimator();
        this.LoadStartPoint();
        this.LoadPlatform();
        this.LoadPlayerHealth();
        this.LoadBuffHeart();
        this.LoadTransform();
    }

    protected virtual void LoadTransform()
    {
        if (this._transform != null) return;
        this._transform = GameObject.Find("Canvas").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadTransform", gameObject);
    }

    protected virtual void LoadBuffHeart()
    {
        if (this.heart != null) return;
        this.heart =GameObject.FindObjectOfType<BuffHeart>();
        //Debug.LogWarning(transform.name + ": LoadBuffHeart", gameObject);
    }

    protected virtual void LoadPlayerHealth()
    {
        if (this.health != null) return;
        this.health = transform.GetComponentInChildren<PlayerHealth>();
        Debug.LogWarning(transform.name + ": LoadPlayerHealth", gameObject);
    }

    protected virtual void LoadPlatform()
    {
        if (this.platform != null) return;
        this.platform = GameObject.FindObjectOfType<Platform>();
        //Debug.LogWarning(transform.name + ": LoadPlatform", gameObject);
    }

    protected virtual void LoadStartPoint()
    {
        if (this.startPoint != null) return;
        this.startPoint = GameObject.FindObjectOfType<StartPoint>();
        Debug.LogWarning(transform.name + ": LoadStartPoint", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this._animator != null) return;
        this._animator = transform.GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadBoxCollider()
    {
        if (this._boxCollider != null) return;
        this._boxCollider = transform.GetComponent<BoxCollider2D>();
        Debug.LogWarning(transform.name + ": LoadBoxCollider", gameObject);
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = transform.GetComponent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidbody2D", gameObject);
    }

    protected virtual void LoadAnimation()
    {
        if (this._animation != null) return;
        this._animation = transform.GetComponentInChildren<Animation>();
        Debug.LogWarning(transform.name + ": LoadAnimation", gameObject);
    }

    protected virtual void LoadMovement()
    {
        if (this.movement != null) return;
        this.movement=transform.GetComponentInChildren<Movement>();
        Debug.LogWarning(transform.name + ": LoadMovement", gameObject);
    }

    public virtual void Jump()
    {
        this.movement.Jump();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Đang ở mặt đất");
            this.movement.Grounded();
            this.ResetAnimationFlags();
            this.startPoint.Animator.SetBool("isPlayerHit", false);
            
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            this.movement.Grounded();
            this.ResetAnimationFlags();
            this.startPoint.Animation_Start.AnimationStart();
        }
         if (collision.gameObject.CompareTag("FallPlatform"))
        {
            this.movement.Grounded();
            this.ResetAnimationFlags();
            //this.movement.FixJump();
            this.ResetAnimationFlags();
            //this.BrowserObj();
            float jumpForce = this.movement.JumpForce;
             jumpForce = 20f;
            //FallPlatform_Animation fallPlatform = collision.gameObject.GetComponent<FallPlatform_Animation>();
            //fallPlatform.ffallPlayform();
        }
        if (collision.gameObject.CompareTag("Traps"))
        {
            this.HandleTouchTheTrap();
            this.movement.Grounded();
            this.ResetAnimationFlags();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            this.HanldeTouchEnemy();
            this.movement.Grounded();
            this.ResetAnimationFlags();
        }

        if (collision.gameObject.CompareTag("Heart"))
        {
            this.movement.Grounded();
            this.ResetAnimationFlags();
        }

        if (collision.gameObject.CompareTag("Traps"))
        {
            int damageAmount = 1;
            this.PlayerHealth.TakeDame(damageAmount);
        }

        if (collision.gameObject.CompareTag("DeadZone")){
            int damageAmount = 3;
            this.PlayerHealth.TakeDame(damageAmount);
        }
    }

    protected virtual void ResetAnimationFlags()
    {
        _animator.SetBool("isDoubleJump", false);
        _animator.SetBool("isJump", false);
    }

    protected virtual void HandleTouchTheTrap()
    {
        //TriggerCustomEvent PlayerDie;
        Debug.Log("Player is die");
    }

    protected virtual void HanldeTouchEnemy()
    {

    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        this.movement.Grounded();
        this.ResetAnimationFlags();
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHandle enemyHandle= other.gameObject.GetComponent<EnemyHandle>();
            if (enemyHandle != null) 
            {
                bool isAlive=enemyHandle.IsAlive;
                isAlive = false;
                enemyHandle.HandleEnemyDie(isAlive);
            }
        }
        if (other.gameObject.CompareTag("Co"))
        {
            //Debug.Log("Next level");
            Transform prefab=this._transform.Find("CompletedLevel");
            prefab.gameObject.SetActive(true);
            //lấy thời gian ở bên ui để sang bên này rồi khi bật prefab lên thì gửi thời gian về cho prefab này
            if (prefab.gameObject.active)
            {
                if(Btn_Level01.Instance!=null)Btn_Level01.Instance.PrcessedWhenEnabled();
                if (Btn_Level02.Instance != null) Btn_Level02.Instance.PrcessedWhenEnabled();
                if (Btn_Level03.Instance != null) Btn_Level03.Instance.PrcessedWhenEnabled();
                if (Btn_Level04.Instance != null) Btn_Level04.Instance.PrcessedWhenEnabled();
            }
        }
        if (other.gameObject.CompareTag("Cup"))
        {
            Debug.Log("Finish!");
        }
        if (other.gameObject.CompareTag("Heart"))
        {
            int buffhp= this.heart.BuffHp;
            this.PlayerHealth.BuffHP(buffhp);
        }
    }
}
