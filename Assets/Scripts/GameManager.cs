using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : VCNVMonoBehaviour
{
    public static GameManager instance { get; private set; }
    private Dictionary<int, LevelResult> levelResults = new Dictionary<int, LevelResult>();

    protected override void Awake()
    {
        base.Awake();
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }  
    
    public void SaveLevelResult(int levelIndex, LevelResult levelResult)
    {
        levelResults[levelIndex] = levelResult;
    }

    public LevelResult GetLevelResult(int levelIndex)
    {
        if (levelResults.ContainsKey(levelIndex)) return levelResults[levelIndex];
        else return null;
    }

    protected virtual void CheckLevelResult(int levelIndex)
    {
        LevelResult result=GameManager.instance.GetLevelResult(levelIndex);
        if (result != null)
        {
            Debug.Log("Completed level " + levelIndex + " with " + result.startsEarned + " stars!");
            Debug.Log("Completion time: " + result.completionTime);
        }
        else
        {
            Debug.Log("No result found for level " + levelIndex);
        }
    }
}
