using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Diese Klasse stuert das Hauptmenu
public class MenuManager : MonoBehaviour
{
    public static bool manual;
    //Alle Szenen
    public enum SceneNumber
    {
        MENU_MAIN = 0,
        PARCOUR_AUTO = 1,
        PARCOUR_MANUAL = 2,
        MENU_COURSE_SELECTION = 3,
        MENU_AI_SELECTION = 4,
        MENU_STATISTICS = 5
    }

    //Lade die Parcours Auswahl
    public void Manual()
    {
        manual = true;
        SceneManager.LoadScene((int)SceneNumber.MENU_COURSE_SELECTION);
    }

    //Lade die Parcours Auswahl
    public void AI()
    {
        manual = false;
        SceneManager.LoadScene((int)SceneNumber.MENU_COURSE_SELECTION);
    }

    //Lade das Statistik Menu
    public void Statistics()
    {
        SceneManager.LoadScene((int)SceneNumber.MENU_STATISTICS);
    }

    //Beende das Programm
    public void Exit()
    {
        Application.Quit();
    }
}