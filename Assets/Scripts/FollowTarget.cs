using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : VCNVMonoBehaviour
{
    [SerializeField] protected Transform target;
    public Transform Target=>target;
    [SerializeField] protected float speed = 2f;

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadTarget();
    }

    protected override void Update()
    {
        base.Update();
        this.Following();
    }

    protected virtual void LoadTarget()
    {
        if (this.target != null) return;
        this.target = GameObject.Find("Player").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadTarget", gameObject);
    }

    protected virtual void Following()
    {
        if (this.target == null) return;
        transform.position = Vector3.Lerp(transform.position,this.target.position, this.speed * Time.deltaTime);
    }
}
