using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : VCNVMonoBehaviour
{
    [SerializeField] protected static Level instance;
    public static Level Instance=>instance;
    [SerializeField] protected List<RectTransform> rectTransforms;
    //[SerializeField] protected CurrentLevel currentLevel;
    private bool[] levelsCompleted;

    protected override void Awake()
    {
        base.Awake();
        if (Level.instance != null) Debug.Log("Just Only 1 Level allow");
        Level.instance = this;
    }

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        //this.LoadCurrentLevel();
        this.LoadRectTransform();
    }

    protected override void Start()
    {
        base.Start();
        this.levelsCompleted=new bool[12];
    }

    //protected virtual void LoadCurrentLevel()
    //{
      //  if (this.currentLevel != null) return;
        //this.currentLevel=transform.Find("Prefabs").GetComponentInChildren<CurrentLevel>();
        //Debug.LogWarning(transform.name + ": LoadCurrentLevel", gameObject);
    //}

    protected virtual void LoadRectTransform()
    {
        if (this.rectTransforms.Count > 0) return;
        Transform prefab = transform.Find("Prefabs");
        if (prefab == null) Debug.Log("prefab is null");
        for (int i = 0; i < prefab.childCount; i++)
        {
            RectTransform rectTransform=prefab.GetChild(i).GetComponent<RectTransform>();
            if(rectTransform != null)
            {
                this.rectTransforms.Add(rectTransform);
            }
        }
    }

    //Phương thức này được gọi khi người hoàn thành 1 màn nào đó
    public virtual void MarkLevelCompleted(int levelIndex)
    {
        if (levelsCompleted[levelIndex])
        {
            CurrentLevel curLevel = transform.Find("Prefabs").GetComponentInChildren<CurrentLevel>();
            if (curLevel != null) curLevel.CurLevel = 1;
        }
        levelsCompleted[levelIndex] = true;
    }

    //Phương thức này kiểm tra xem một màn cụ thể có hoàn thành chưa
    public virtual bool IsLevelCompleted(int levelIndex)
    {
        return levelsCompleted[levelIndex]&& (levelIndex==0||levelsCompleted[levelIndex-1]);
    }
}
