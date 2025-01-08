using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : VCNVMonoBehaviour
{
    [SerializeField] protected int maxHealth = 3;
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected Hearts hearts; 

    protected override void Start()
    {
        base.Start();
        this.BuffHealth();
        this.UpdateHealUI();
    }

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadHearts();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void LoadHearts()
    {
        if (this.hearts != null) return;
        this.hearts=GameObject.FindObjectOfType<Hearts>();
        Debug.LogWarning(transform.name + ": LoadHearts", gameObject);
    }

    protected virtual void BuffHealth()
    {
        this.currentHealth = this.maxHealth;
    }

    protected virtual void UpdateHealUI()
    {
        if (this.hearts == null) return;

        for(int i=0;i<this.hearts.HeartUI.Count;i++)
        {
            if (i < currentHealth)
            {
                this.hearts.HeartUI[i].gameObject.SetActive(true);
            }
            else
            {
                this.hearts.HeartUI[i].gameObject.SetActive(false);
            }
        }
    }

    public virtual void TakeDame(int damageAmount)
    {
        this.currentHealth -= damageAmount;
        if(this.currentHealth < 0)this.currentHealth = 0;

        this.UpdateHealUI();

        if (this.currentHealth == 0) this.PlayerDie();
    }

    protected virtual void PlayerDie()
    {
        //Debug.Log("Player hết máu");
        Transform deadUI= this.playerCtrl._Transform.Find("DeadUI");
        if (deadUI == null) Debug.Log("deadUI is null");
        deadUI.gameObject.SetActive(true);
    }

    public virtual void BuffHP(int hpBuff)
    {
        this.currentHealth += hpBuff;
        if (this.currentHealth > maxHealth)
        {
            this.currentHealth = maxHealth;
        }

        this.UpdateHealUI();
    }

    
}
