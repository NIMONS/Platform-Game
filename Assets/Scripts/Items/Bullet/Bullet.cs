using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : VCNVMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected int damageAmount = 1;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadPlayerCtrl();
    }

    protected override void Update()
    {
        base.Update();
        this.DestroyObj();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl=GameObject.FindObjectOfType<PlayerCtrl>();
        //Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void DestroyObj()
    {
        Invoke("DestroyOBJ", 2f);
    }

    protected virtual void DestroyOBJ()
    {
        Destroy(transform.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trừ máu player");

            this.playerCtrl.PlayerHealth.TakeDame(this.damageAmount);
            this.DestroyOBJ();
        }
    }
}
