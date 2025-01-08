using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class CurrentLevel : VCNVMonoBehaviour
{
    [SerializeField] protected Transform blockLevel;
    [SerializeField] protected Level _level;
    [SerializeField] protected int curLevel = 0;//=0 là chưa hoàn thành =1 là hoàn thành
    public int CurLevel { set; get; }
    [SerializeField] protected int curMap = 1;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadLevel();
        this.LoadBlockLevel();
    }

    protected override void Start()
    {
        base.Start();
        this.SetLevel();
    }

    protected virtual void LoadBlockLevel()
    {
        if (this.blockLevel != null) return;
        this.blockLevel = transform.Find("BockLevel");
        Debug.LogWarning(transform.name + ": LoadBlockLevel", gameObject);
    }

    protected virtual void LoadLevel()
    {
        if (this._level != null) return;
        this._level=transform.parent.parent.GetComponent<Level>();
        Debug.LogWarning(transform.name + ": LoadLevel", gameObject);
    }

    public virtual int checkTrue(int curLevel)
    {
        if (curLevel == 1) return 1;
        else return 0;
    }

    protected virtual void SetLevel()
    {
        Transform btnPlay = transform.Find("Button_Play");
        Button evenBtn= btnPlay.GetComponent<Button>();
        ColorBlock colors = evenBtn.colors;

        if (btnPlay == null) Debug.Log("btnPlay is null");
        if (evenBtn == null) Debug.Log("evenBtn is null");

        if (this.curLevel == 1)
        {
            this._level.MarkLevelCompleted(this.curMap);
            this.blockLevel.transform.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Bạn phải hoàn thành màn chơi trước để chơi màn này");
            evenBtn.interactable = false;
            this.blockLevel.transform.gameObject.SetActive(true);
            colors.disabledColor = new Color(123f, 123f, 123f, 255f);
            evenBtn.colors = colors;
        }
    }

}
