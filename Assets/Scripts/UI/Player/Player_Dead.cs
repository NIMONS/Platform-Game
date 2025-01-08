using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Dead : VCNVMonoBehaviour
{
    [SerializeField] protected GameObject deadUiGameObj;
    public GameObject DeadUiGameObj=>deadUiGameObj;

    [SerializeField] protected PlayerCtrl playerCtrl;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadPlayerCtrl();
    }

    CustomEvent PlayerDie;
    //custome Event Arguments
    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl=GameObject.FindObjectOfType<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }
}
