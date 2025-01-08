using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerr : VCNVMonoBehaviour
{
    [SerializeField] private static LevelManagerr instance;
    public static LevelManagerr Instance => instance;
    [SerializeField] protected List<Transform> btns;
    [SerializeField] protected List<Button> levelButtons;
    [SerializeField] protected int currentLevel = 1;
    [SerializeField] protected int toalLevels = 12;
    private bool[] levelCompleted;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadButton();
    }

    protected override void Awake()
    {
        base.Awake();
        if (LevelManagerr.instance != null)
        {
            Debug.Log("Just only 1 LevelManagerr allow");
            Destroy(gameObject);
        }
        LevelManagerr.instance = this;
        this.levelCompleted=new bool[toalLevels];
    }

    protected override void Start()
    {
        base.Start();
        this.LoadSavedLevel();
        this.LoadBtnTransform();
        this.UpdateLevelButtons();
    }

    //Load Button nằm trong các obj UiLevel
    protected virtual void LoadBtnTransform()
    {
        foreach(Transform btnTransform in btns)
        {
            Button btn=btnTransform.GetComponentInChildren<Button>();
            if (btn != null)
            {
                this.levelButtons.Add(btn);
            }
            else
            {
                Debug.LogWarning("Không tìm thấy Button trong " + btnTransform.name);
            }
        }
    }


    protected virtual void LoadButton()
    {
        if (this.btns.Count>0) return;
        Transform prefab = transform.Find("Prefabs");
        foreach(Transform b in prefab)
        {
            this.btns.Add(b);
        }
        Debug.LogWarning(transform.name + ": LoadButton", gameObject);
    }

    protected virtual void LoadSavedLevel()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }

        //this.LoadLevel(currentLevel);
    }

    //sửa lại phương thức này để tránh việc người chơi 1 màn 2 lần sẽ mở khóa các màn sau
    public virtual void CompleteLevel()
    {
        if (IsLevelCompleted(currentLevel - 1))
        {
            this.SaveCurrentLevel();
            return;
        }

        // Chỉ tăng currentLevel nếu màn chưa hoàn thành và màn trước đó đã hoàn thành
        if (!IsLevelCompleted(currentLevel))
        {
            this.currentLevel++;
            if (this.currentLevel <= this.toalLevels)
            {
                MarkLevelCompleted(currentLevel-1); // Đánh dấu màn hiện tại là đã hoàn thành
                SaveCurrentLevel();
            }
        }
    }

    protected virtual void SaveCurrentLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.Save();
    }

    protected virtual bool IsLevelCompleted(int levelIndex)
    {
        if (levelIndex > 0 && levelIndex < this.toalLevels)
        {
            if (levelCompleted != null && levelIndex < levelCompleted.Length)
            {
                //Debug.Log(levelCompleted[levelIndex]);
                return levelCompleted[levelIndex];
            }
            else
            {
                Debug.LogWarning("levelCompleted array is not initialized properly or levelIndex is out of range.");
                return false;
            }
        }
        else
        {
            //Debug.LogWarning("Invalid levelIndex: " + levelIndex);
            return false;
        }
    }

    public virtual void MarkLevelCompleted(int levelIndex)
    {
        if (levelIndex < 0 && levelIndex > this.toalLevels)
        {
            Debug.Log("Error: Invalid level index!");
            return;
        }
        levelCompleted[levelIndex] = true;
    }

    protected virtual void UpdateLevelButtons()
    {
        for(int i=0;i< levelButtons.Count;i++)
        {
            if (i < currentLevel)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
