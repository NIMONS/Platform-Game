using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JunkCtrl : VCNVMonoBehaviour
{
    [SerializeField] protected Despawn despawn;
    public Despawn Despawn => despawn;
    [SerializeField] protected SpawnPoint spawnPoint;
    public SpawnPoint SpawnPoint => spawnPoint;
    [SerializeField] protected Spawner spawner;
    public Spawner Spawner => spawner;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadDespawn();
        this.LoadSpawnPoint();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = transform.GetComponent<Spawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }

    protected virtual void LoadSpawnPoint()
    {
        if (this.spawnPoint != null) return;
        this.spawnPoint = GameObject.FindObjectOfType<SpawnPoint>();
        Debug.LogWarning(transform.name + ": LoadSpawnPoint", gameObject);
    }

    protected virtual void LoadDespawn()
    {
        if (this.despawn != null) return;
        this.despawn= GameObject.FindObjectOfType<Despawn>();
        Debug.LogWarning(transform.name + ": LoadDespawn", gameObject);
    }

}
