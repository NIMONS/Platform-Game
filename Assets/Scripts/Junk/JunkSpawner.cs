using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawner : Spawner
{
    private static JunkSpawner instance;
    public JunkSpawner Instance=>instance;

    protected override void Awake()
    {
        base.Awake();
        if (JunkSpawner.instance != null) Debug.Log("Just only 1 JunkSpawner allow");
        JunkSpawner.instance = this;    
    }
}
