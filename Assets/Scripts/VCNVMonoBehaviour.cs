using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCNVMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        this.LoadCompoments();
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.DeleteAll();
        //có thể sử dụng DeleteAll để xóa toàn bộ dữ liệu được lưu trong PlayerPrefs
    }

    protected virtual void Start()
    {
        //for ovveride
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void Awake()
    {
        this.LoadCompoments();
    }

    protected virtual void LoadCompoments()
    {

    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable() 
    {

    }
}
