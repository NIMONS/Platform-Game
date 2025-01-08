using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform_Animation : VCNVMonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rigidbody2;
    public Rigidbody2D Rigidbody2D=> _rigidbody2;
    [SerializeField] protected FallPlayform fallPlayform;
    public FallPlayform FallPlayform=> fallPlayform;
    [SerializeField] protected BoxCollider2D _boxCollider2D;
    public BoxCollider2D BoxCollider2D=> _boxCollider2D;
    [SerializeField] protected float fallDelay = 2f;//thời gian chờ khi nhân vật đã nhảy lên vật thể
    [SerializeField] protected float fallSpeed = 0.1f;//tốc độ rơi của vật thể
    [SerializeField] protected int VelocityValue = 5;
    [SerializeField] protected GameObject FallPlatformObj;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadFallPlayform();
        this.LoadRigidbody2D();
        this.LoadBoxCollider2D();
    }

    protected virtual void LoadBoxCollider2D()
    {
        if (this._boxCollider2D != null) return;
        this._boxCollider2D = transform.GetComponent<BoxCollider2D>();
        Debug.LogWarning(transform.name + ": LoadBoxCollider2D", gameObject);
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this._rigidbody2 != null) return;
        this._rigidbody2 = transform.GetComponent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidbody2D", gameObject);
    }

    protected virtual void LoadFallPlayform()
    {
        if (this.fallPlayform != null) return;
        this.fallPlayform=transform.parent.GetComponent<FallPlayform>();
        Debug.LogWarning(transform.name + ": fallPlayform", gameObject);
    }

    public virtual void ffallPlayform()
    {
        Animator animator = this.fallPlayform.Animator;
        if (animator != null) animator.SetTrigger("Hit");
        Debug.Log("vật thể rơi");
        StartCoroutine(FallAfterDelay());
    }

    IEnumerator FallAfterDelay()
    {
        //đợi một khoảng thời gian trước khi bắt đầu rơi
        yield return new WaitForSeconds(fallDelay);

        //rơi xuống
        if (this._rigidbody2 != null)
        {
            this._rigidbody2.gravityScale = 0.1f;//kích hoạt hiệu ứng trọng lực
            this._rigidbody2.velocity = new Vector2(0,-fallSpeed);//tốc độ rơi
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Vật thể rơi chạm đất");
            this.BoxCollider2D.isTrigger = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            //this.ffallPlayform();
            this._rigidbody2.gravityScale = this.VelocityValue;
            //gọi hàm Destroy() sau 0.5s
            Invoke("DestroyFallPlatform", 1f);
        }
    }

    protected virtual void DestroyFallPlatform()
    {
        //Hủy đối tượng FallPlatformObj sau 0.5 giấy
        Destroy(this.FallPlatformObj.gameObject);
    }
}
