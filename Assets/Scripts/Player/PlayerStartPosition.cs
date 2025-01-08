using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPosition : VCNVMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;

    protected override void Start()
    {
        base.Start();
        this.StartPointPlayer();
    }

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void StartPointPlayer()
    {
        var startPoint= this.playerCtrl.StartPoint.transform.position;
        if(startPoint != null)
        {
            Vector3 newPos = transform.parent.position;
            newPos.x=startPoint.x+0.6f;
            newPos.y = startPoint.y + 0.1f;

            transform.parent.position= newPos;
        }
        else
        {
            Debug.Log("Không có điểm bắt đầu được thiết lập cho nhân vật!");
        }
    }
}
