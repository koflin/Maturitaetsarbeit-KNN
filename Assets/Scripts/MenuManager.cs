using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Manual()
    {
        SceneManager.LoadScene(1);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

}