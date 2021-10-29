using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleScene : MonoBehaviour
{
    public string loadLevel;

    public void LoadScene()
    {
        SceneManager.LoadScene(loadLevel);
    }
}
