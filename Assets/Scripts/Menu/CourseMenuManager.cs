using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MenuManager;

public class CourseMenuManager : MonoBehaviour {

    public static string storagePath;

    public GameObject courseItemPrefab;
    public GameObject itemContainer;
    public List<GameObject> items;
    public int chosenIndex = 0;

	// Use this for initialization
	void Start () {
        /*storagePath = Application.dataPath + "/Resources/Courses";

        string[] files = Directory.GetFiles(storagePath);

        items = new List<GameObject>();

        int i = 0;
        foreach (string file in files)
        {
            if (Path.GetExtension(file) == ".meta")
            {
                continue;
            }

            GameObject item = Instantiate(courseItemPrefab, itemContainer.transform);

            item.transform.GetChild(0).GetComponent<Text>().text = Path.GetFileNameWithoutExtension(file);

            //Es muss eine Kopie von i gemacht werden, da AddListener anscheinend asynchron verläuft
            int copyI = i;
            item.GetComponent<Button>().onClick.AddListener(() => ChooseCourse(copyI));

            items.Add(item);
            i++;
        }*/

        GameObject[] courses = Resources.LoadAll<GameObject>("Courses");

        int i = 0;
        foreach (GameObject course in courses)
        {
            GameObject item = Instantiate(courseItemPrefab, itemContainer.transform);

            item.transform.GetChild(0).GetComponent<Text>().text = course.name;

            //Es muss eine Kopie von i gemacht werden, da AddListener anscheinend asynchron verläuft
            int copyI = i;
            item.GetComponent<Button>().onClick.AddListener(() => ChooseCourse(copyI));

            items.Add(item);
            i++;
        }

        items[0].GetComponent<Button>().Select();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChooseCourse(int indexClicked)
    {
        items[indexClicked].GetComponent<Button>().Select();
    }

    public void Play()
    {
        TrainingController.courseId = items[chosenIndex].transform.GetChild(0).GetComponent<Text>().text;

        if (MenuManager.manual)
        {
            SceneManager.LoadScene((int)SceneNumber.PARCOUR_MANUAL);
        }

        else
        {
            //SceneManager.LoadScene((int)SceneNumber.MENU_AI_SELECTION);
            SceneManager.LoadScene((int)SceneNumber.PARCOUR_AUTO);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene((int)SceneNumber.MENU_MAIN);
    }
}
