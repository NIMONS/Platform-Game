using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : VCNVMonoBehaviour
{
    [SerializeField] protected List<Transform> starsUI;
    public List<Transform> StarsUI=>starsUI;
    protected Timer timer;

    protected override void Start()
    {
        base.Start();
        //timer = Timer.Instance;
        //if (timer == null) Debug.Log("timer is null");
        //Timer.Instance.OnTimerFinished+=this.HandleTimerFinished;
    }
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadStarUI();
    }

    protected virtual void LoadStarUI()
    {
        if (this.starsUI.Count > 0) return;
        Transform prefab = transform.Find("Prefabs");
        foreach(Transform i in prefab)
        {
            this.starsUI.Add(i);
            this.HidePrefab(this.starsUI.Count-1);
        }
        Debug.Log(transform.name + ": LoadStarUI", gameObject);
    }

    protected virtual void HidePrefab(int index)
    {
        if(index>=0&&index<this.starsUI.Count)
        {
            this.starsUI[index].gameObject.SetActive(false);
        }
    }

    protected virtual void HandleTimerFinished(float time)
    {
        //xử lý kết quả dựa trên thời gian hoàn thành
        if (time<=30.0f)
        {
            this.starsUI[2].gameObject.SetActive(true);
        }
        else if(time>30.0f&&time<=45.0f)
        {
            this.starsUI[1].gameObject.SetActive(true);

        }
        else if (time > 60.0f)
        {
            this.starsUI[0].gameObject.SetActive(true);
        }
    }

    //protected virtual void OnDestroy()
    ////{
     //   timer.OnTimerFinished-=this.HandleTimerFinished;
   // }
}
