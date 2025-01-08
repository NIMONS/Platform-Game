using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawn : VCNVMonoBehaviour
{
    [SerializeField] protected JunkCtrl junkCtrl;
    public JunkCtrl JunkCtrl=>junkCtrl;

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadJunkCtrl();
    }

    protected override void Start()
    {
        base.Start();
        this.SpawnJunk();
    }

    protected virtual void LoadJunkCtrl()
    {
        if (this.junkCtrl != null) return;
        this.junkCtrl=transform.GetComponent<JunkCtrl>();
        Debug.LogWarning(transform.name + ": LoadJunkCtrl", gameObject);
    }

    protected virtual void SpawnJunk()
    {
        List<Transform> spawnPoints=this.junkCtrl.SpawnPoint.GetSpawnPoints();
        foreach (Transform points in spawnPoints)
        {
            Transform prefab = this.junkCtrl.Spawner.GetRanPrefab();
            Transform junk = this.InstantiateJunk(points.position, prefab);
        }
    }

    protected virtual Transform InstantiateJunk(Vector3 spawnPoints,Transform prefab)
    {
        Quaternion spawnRot = Quaternion.identity;
        Transform newJunk=this.junkCtrl.Spawner.Spawn(prefab,spawnPoints,spawnRot);
        newJunk.gameObject.SetActive(true);
        return newJunk;
    }
}
