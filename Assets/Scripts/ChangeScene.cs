using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene1()
    {
    Debug.Log("LoadScene1 called");
        SceneManager.LoadScene("Scene1");
    }
    public void LoadScene2()
    {
    Debug.Log("LoadScene2 called");
        SceneManager.LoadScene("SceneRainbow");
    }
    public void LoadScene3()
    {
    Debug.Log("LoadScene3 called");
        SceneManager.LoadScene("SceneFrame");
    }
}
