using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : VCNVMonoBehaviour
{
    [SerializeField] protected List<Transform> levels;
    public List<Transform> Levels=>levels;

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadLevel();
    }

    protected virtual void LoadLevel()
    {
        if (this.levels.Count > 0) return;
        Transform prefab = transform.Find("Prefabs");
        foreach(Transform t in prefab)
        {
            this.levels.Add(t);
        }
        Debug.LogWarning(transform.name + ": prefab", gameObject);
    }
}
