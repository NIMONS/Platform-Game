using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn_Back : BaseButton
{
    [SerializeField] protected string nameSceneToLoad;
    protected override void OnClick()
    {
        //Debug.Log("Button đã được nhấn!");
        SceneManager.LoadScene(nameSceneToLoad);
    }
}
