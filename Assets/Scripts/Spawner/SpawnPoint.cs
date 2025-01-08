using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : VCNVMonoBehaviour
{
    [SerializeField] protected List<Transform> points;
    public List<Transform> Points=>points;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadPoints();
    }

    protected virtual void LoadPoints()
    {
        if (this.points.Count > 0) return;
        foreach(Transform point in transform)
        {
            this.points.Add(point);
        }
        Debug.LogWarning(transform.name + ": LoadPoints", gameObject);
    }

    public virtual List<Transform> GetSpawnPoints()
    {
        if (this.points.Count == 0) return null;
        return this.points;
    }

}
