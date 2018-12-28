using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MenuManager;

public class AIMenuManager : MonoBehaviour {

    public GameObject toogleTesting;
    public GameObject aiNewPrefab;
    public string newName;
    public GameObject aiPrefab;
    public GameObject itemContainer;
    public GameObject[] items;
    public List<AI> ais;
    public int chosenIndex;

    // Use this for initialization
    void Start()
    {
        ais = AI.GetAll();

        items = new GameObject[ais.Count + 1];
        for (int i = 0; i < ais.Count + 1; i++)
        {
            //Es muss eine Kopie von i gemacht werden, da AddListener anscheinend asynchron verläuft
            int copyI = i;

            GameObject item;
        
            if (i == 0)
            {
                item = Instantiate(aiNewPrefab, itemContainer.transform);

                item.transform.GetChild(1).GetComponent<InputField>().onEndEdit.AddListener((string input) => ChooseName(input));
            }

            else
            {
                item = Instantiate(aiPrefab, itemContainer.transform);

                item.transform.GetChild(0).GetComponent<Text>().text = ais[i - 1].name;

                item.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => DeleteAI(copyI));
            }

            item.GetComponent<Button>().onClick.AddListener(() => ChooseAI(copyI));

            items[i] = item;
        }

        items[0].GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChooseAI(int indexClicked)
    {
        chosenIndex = indexClicked;
        items[indexClicked].GetComponent<Button>().Select();

        if (indexClicked == 0)
        {
            toogleTesting.GetComponent<Toggle>().interactable = false;
        }

        else
        {
            toogleTesting.GetComponent<Toggle>().interactable = true;
        }
    }

    public void ChooseName(string name)
    {
        newName = name;
    }

    public void DeleteAI(int indexClicked)
    {
        chosenIndex = indexClicked;
        ais[indexClicked - 1].Delete();

        Destroy(items[indexClicked]);
    }

    public void Next()
    {
        if (chosenIndex == 0)
        {
            TrainingController.ai = new AI(newName);
            
        }

        else
        {
            TrainingController.ai = ais[chosenIndex - 1];

            if (toogleTesting.GetComponent<Toggle>().isOn)
            {
                GeneticAlgorithm.testing = true;
            }

            else
            {
                GeneticAlgorithm.testing = false;
            }
        }


        SceneManager.LoadScene((int)SceneNumber.PARCOUR_AUTO);
    }

    public void Back()
    {
        SceneManager.LoadScene((int)SceneNumber.MENU_COURSE_SELECTION);
    }
}