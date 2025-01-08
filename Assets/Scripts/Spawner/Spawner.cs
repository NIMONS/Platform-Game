using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : VCNVMonoBehaviour
{
    [SerializeField] protected Transform holder;
    public Transform Holder=> holder;
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolObj;
    [SerializeField] protected int spawnCount = 0;
    public int SpawnCount=> spawnCount;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadPrefabs();
        this.LoadHolder();
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        Transform newPrefab = transform.Find("Prefabs");
        foreach(Transform prefab in newPrefab)
        {
            this.prefabs.Add(prefab);
        }
        this.HidePrefabs();
        Debug.LogWarning(transform.name + ": LoadPrefabs", gameObject);
    }

    protected virtual void HidePrefabs()
    {
        foreach(Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        Debug.LogWarning(transform.name + ": LoadHolder", gameObject);
    }

    public virtual Transform Spawn(string namePrefabs,Vector3 spawnPos,Quaternion spawnRot)
    {
        Transform newPrefab = this.GetPrefabByName(namePrefabs);
        if (newPrefab == null) Debug.Log("Prefabs not found: " + namePrefabs);
        return this.Spawn(newPrefab, spawnPos, spawnRot);
    }

    public virtual Transform Spawn(Transform newPrefab,Vector3 spawnPos,Quaternion spawnRot)
    {
        Transform prefab = this.GetObjFromPooj(newPrefab);
        prefab.SetPositionAndRotation(spawnPos, spawnRot);

        prefab.SetParent(this.holder);
        this.spawnCount++;
        return prefab;
    }

    //lấy obj từ pooj ra
    protected virtual Transform GetObjFromPooj(Transform newPrefab)
    {
        foreach(Transform poolObj in this.poolObj)
        {
            if (poolObj == null) continue;
            if (poolObj.name == newPrefab.name)
            {
                this.poolObj.Remove(poolObj);
                return poolObj;
            }
        }

        Transform prefab=Instantiate(newPrefab);
        prefab.name= newPrefab.name;
        return prefab;
    }

    //lấy obj theo tên
    public virtual Transform GetPrefabByName(string namePrefabs)
    {
        foreach(Transform prefab in this.prefabs)
        {
            if (prefab.name == namePrefabs) return prefab;
        }
        return null;
    }

    public virtual Transform GetRanPrefab()
    {
        int ran = Random.Range(0, this.prefabs.Count);
        return this.prefabs[ran];
    }

    public virtual void Despawn(Transform obj)
    {
        if (this.poolObj.Contains(obj)) return;
        this.poolObj.Add(obj);
        obj.gameObject.SetActive(false);
        this.spawnCount--;
    }
}
