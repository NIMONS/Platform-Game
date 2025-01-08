using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsResult : VCNVMonoBehaviour
{
    //[SerializeField] protected PlayerCtrl playerCtrl;
    //public PlayerCtrl PlayerCtrl => playerCtrl;
    [SerializeField] protected string CompletedStarsKey;
    [SerializeField] protected List<Transform> stars;
    public List<Transform> Stars =>stars;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadStars();
        //this.LoadPlayerCtrl();
    }

    protected override void Start()
    {
        base.Start();
        this.ActiveStars();
    }

    //protected virtual void LoadPlayerCtrl()
    //{
      //  if (this.playerCtrl != null) return;
      //  this.playerCtrl = GameObject.FindObjectOfType<PlayerCtrl>();
      //  Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
   // }

    protected virtual void LoadStars()
    {
        if (this.stars.Count>0) return;
        Transform prefab = transform.Find("Prefabs");
        foreach(Transform i in prefab)
        {
            this.stars.Add(i);
        }
        this.HideStars();
        Debug.LogWarning(transform.name + ": LoadStars", gameObject);
    }

    protected virtual void HideStars()
    {
        foreach(Transform i in this.stars)
        {
            i.gameObject.SetActive(false);
        }
    }

    protected virtual int GetCompletedStars()
    {
        if (Btn_Level01.Instance != null)
        {
            return PlayerPrefs.GetInt("SaveStars01", 0);
        }
        if (Btn_Level02.Instance != null)
        {
            return PlayerPrefs.GetInt("SaveStars02", 0);
        }
        if (Btn_Level03.Instance != null)
        {
            return PlayerPrefs.GetInt("SaveStars03", 0);
        }
        if (Btn_Level04.Instance != null)
        {
            return PlayerPrefs.GetInt("SaveStars04", 0);
        }
        return 0;
    }

    protected virtual void ActiveStars()
    {
        int numStar=this.GetCompletedStars();
        Debug.Log("số sao cho phép là: " + numStar);
        for(int i=0; i < numStar; i++)
        {
            if (this.stars[i] != null)
            {
                this.stars[i].gameObject.SetActive(true);
            }
        }
    }
}
