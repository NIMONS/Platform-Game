using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn_PlayAgain : BaseButton
{
    [SerializeField] protected string nameSceneToLoad;
    protected override void OnClick()
    {
        //Debug.Log("Button đã được nhấn!");
        SceneManager.LoadScene(nameSceneToLoad);
    }
}
