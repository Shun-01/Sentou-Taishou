using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ToReplay()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("TitleScene"); 
    }

    public void ToGame()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    public void ToResult()
    {
        SceneManager.LoadScene("Result");
    }
}
