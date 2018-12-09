using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool manual;
    public enum SceneNumber
    {
        MENU_MAIN = 0,
        PARCOUR_AUTO = 1,
        PARCOUR_MANUAL = 2,
        MENU_COURSE_SELECTION = 3,
        MENU_AI_SELECTION = 4,
        MENU_STATISTICS = 5
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Manual()
    {
        manual = true;
        SceneManager.LoadScene((int)SceneNumber.MENU_COURSE_SELECTION);
    }

    public void AI()
    {
        manual = false;
        SceneManager.LoadScene((int)SceneNumber.MENU_COURSE_SELECTION);
    }

    public void Statistics()
    {
        SceneManager.LoadScene((int)SceneNumber.MENU_STATISTICS);
    }

    public void Exit()
    {
        Application.Quit();
    }
}