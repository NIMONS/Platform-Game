using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class TimeResult : VCNVMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI text;
    public TextMeshProUGUI Text => text;
    private static TimeResult instance;
    public static TimeResult Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) return;
        instance = this;
    }

    protected override void LoadCompoments()
    {
        base.LoadCompoments();
        this.LoadText();
    }

    protected virtual void LoadText()
    {
        if (this.text != null) return;
        this.text=transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadText", gameObject);
    }
    public virtual void SaveTime(float time)
    {
        float roundedTime = Mathf.Round(time * 100f) / 100f;
        this.text.text = roundedTime.ToString();
        Debug.Log("Thơi gian hoàn thành màn: " + this.text);
    }
}
