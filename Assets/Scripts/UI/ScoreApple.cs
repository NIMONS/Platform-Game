using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreApple : VCNVMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI text;
    public TextMeshProUGUI Text => text;
    [SerializeField] protected Score score;

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadText();
        this.LoadScore();
    }

    protected virtual void LoadScore()
    {
        if (this.score != null) return;
        this.score = transform.parent.parent.GetComponent<Score>();
        Debug.LogWarning(transform.name + ": LoadScore", gameObject);
    }

    protected virtual void LoadText()
    {
        if (this.text != null) return;
        this.text = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadText", gameObject);
    }

    public virtual void AppleResult(int score)
    {
        if (score == 0) return;
        this.text.text = score.ToString();
    }
}
