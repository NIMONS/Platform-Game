using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : VCNVMonoBehaviour
{
    [SerializeField] protected List<Transform> fallPlatforms;
    public List<Transform> FallPlatforms=> fallPlatforms;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadFallPlatforms();
    }

    protected virtual void LoadFallPlatforms()
    {
        if (this.fallPlatforms.Count > 0) return;
        Transform newPrefab = transform.Find("Prefabs");
        foreach(Transform fallPlatform in newPrefab)
        {
            this.fallPlatforms.Add(fallPlatform);
        }
        Debug.LogWarning(transform.name+ ": LoadFallPlatforms",gameObject);
    }
}
