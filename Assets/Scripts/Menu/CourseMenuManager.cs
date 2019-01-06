using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MenuManager;

//Diese Klasse steuert das Parcours Menu
public class CourseMenuManager : MonoBehaviour {

    public static string storagePath;

    public GameObject courseItemPrefab;
    public GameObject itemContainer;
    public List<GameObject> items;
    public int chosenIndex = 0;

	//Wird beim Start ausgeführt
	void Start () {
        //Lade alle verfügbaren Parcours
        GameObject[] courses = Resources.LoadAll<GameObject>("Courses");

        int i = 0;
        //Loope durch jeden Parcours
        foreach (GameObject course in courses)
        {
            //Erstellt eine Parcours Spalte im Menu
            GameObject item = Instantiate(courseItemPrefab, itemContainer.transform);

            item.transform.GetChild(0).GetComponent<Text>().text = course.name;

            //Es muss eine Kopie von i gemacht werden, da AddListener anscheinend asynchron verläuft
            int copyI = i;
            //Setzte die Aktion welche ausgeführt wird beim klicken auf den Parcours
            item.GetComponent<Button>().onClick.AddListener(() => ChooseCourse(copyI));

            items.Add(item);
            i++;
        }

        items[0].GetComponent<Button>().Select();
	}

    //Wird ausgeführt wenn auf eine Parcours Spalte geklickt wird
    public void ChooseCourse(int indexClicked)
    {
        chosenIndex = indexClicked;
        //Wähle die geklickte Parcours Spalte
        items[indexClicked].GetComponent<Button>().Select();
    }

    //Gehe zum Spiel
    public void Play()
    {
        TrainingController.courseId = items[chosenIndex].transform.GetChild(0).GetComponent<Text>().text;

        //Wenn manuelle Steuerung aktiviert ist lade manuelle Parcours Szene sonst lade KI Auswahl
        if (MenuManager.manual)
        {
            SceneManager.LoadScene((int)SceneNumber.PARCOUR_MANUAL);
        }

        else
        {
            SceneManager.LoadScene((int)SceneNumber.MENU_AI_SELECTION);
        }
    }

    //Gehe zurück im Menu
    public void Back()
    {
        SceneManager.LoadScene((int)SceneNumber.MENU_MAIN);
    }
}
