using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlantHandle : VCNVMonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected GameObject enemyGameObj;
    [SerializeField] protected GameObject ShootPoint;
    [SerializeField] protected GameObject Bullets;
    [SerializeField] protected bool isAlive = true;
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float timeDelay = 2f;
    [SerializeField] protected float speedBullet = 3f;
    [SerializeField] protected float direction = 0;//nếu là 0 thì bên trái
    protected override void Update()
    {
        base.Update();
        this.HandlePlantAttack_Animation();
    }

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        if (this._animator != null) return;
        this._animator=transform.GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void HandlePlantAttack_Animation()
    {
        if (!this.isAlive) return;
        this.timer += Time.deltaTime;
        if (this.timer>=this.timeDelay)
        {
            this._animator.SetBool("isAttacking", true);
            StartCoroutine(this.WaitForEnemyAttack());
            this.timer = 0f;
        }
    }

    protected virtual void SpeedBullet()
    {
        GameObject bulletInstance= Instantiate(this.Bullets, this.ShootPoint.transform.position, Quaternion.identity);

        Rigidbody2D bulletRigibody= bulletInstance.GetComponent<Rigidbody2D>();
        
        if (this.direction == 0)
        {
            float temp = -1f;
            bulletRigibody.velocity = new Vector2(temp * this.speedBullet, 0f);
        }else if(this.direction == 1)
        {
            float temp = 1f;
            bulletRigibody.velocity = new Vector2(temp * this.speedBullet, 0f);
        }
    }

    IEnumerator WaitForEnemyAttack()
    {
        this.SpeedBullet();
        yield return new WaitForSeconds(this._animator.GetCurrentAnimatorStateInfo(0).length);
        this._animator.SetBool("isAttacking", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this._animator.SetTrigger("Hit");
            Invoke("DestroyObj", 0.4f);
        }
    }

    protected virtual void DestroyObj()
    {
        Destroy(transform.parent.gameObject);
    }
    
}
