using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn_MainMenu : BaseButton
{
    [SerializeField] protected string sceneNameToLoad;
    protected override void OnClick()
    {
        //có thể truyền sceneNameToLoad vào LoadScene
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
