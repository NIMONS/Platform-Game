using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : VCNVMonoBehaviour
{
    [SerializeField] protected List<Transform> heartUI;
    public List<Transform> HeartUI => heartUI;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadHeartUi();
    }

    protected virtual void LoadHeartUi()
    {
        if (this.heartUI.Count>0) return;
        Transform newPre = transform.Find("Prefabs");
        foreach(Transform i in newPre)
        {
            this.heartUI.Add(i);
        }
        Debug.LogWarning(transform.name + ": LoadHeartUi", gameObject);
    }
}
