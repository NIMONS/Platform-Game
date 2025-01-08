using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UILevels : VCNVMonoBehaviour
{
    [SerializeField] protected List<Transform> stars;
    public List<Transform> Stars => stars;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadStars();
    }

    protected virtual void LoadStars()
    {
        if (this.stars.Count > 0) return;
        Transform prefab = transform.Find("Star_Score").Find("Prefabs");
        if (prefab != null)
        {
            foreach (Transform t in prefab)
            {
                this.stars.Add(t);
            }
        }
        this.HideStars();
        Debug.LogWarning(transform.name + ": LoadStars", gameObject);
    }

    protected virtual void HideStars()
    {
        foreach (Transform t in this.stars)
        {
            t.gameObject.SetActive(false);
        }
    }
}
