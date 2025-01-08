using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : VCNVMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI text;
    public TextMeshProUGUI Text => text;
    [SerializeField] protected int score = 0;
    public int Sccore => score;
    [SerializeField] protected ScoreApple scoreApple;
    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadText();
        this.LoadScoreApple();
    }

    protected virtual void LoadScoreApple()
    {
        if (this.scoreApple != null) return;
        this.scoreApple = transform.Find("CompletedLevel").Find("ScoreApple").GetComponentInChildren<ScoreApple>();
        Debug.LogWarning(transform.name + ": LoadScoreApple", gameObject);
    }

    protected virtual void LoadText()
    {
        if (this.text != null) return;
        this.text=transform.Find("PointUI").GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadText", gameObject);
    }

    public virtual void PickupItem()
    {
        score++;
        if(this.text!=null)text.text=score.ToString("00");
        this.scoreApple.AppleResult(this.score);
    }

}
