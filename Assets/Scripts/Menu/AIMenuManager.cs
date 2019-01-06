using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MenuManager;

//Dise Klasse steuert das KI Menu
public class AIMenuManager : MonoBehaviour {

    public GameObject toogleTesting;
    public GameObject aiNewPrefab;
    public string newName;
    public GameObject aiPrefab;
    public GameObject itemContainer;
    public GameObject[] items;
    public List<AI> ais;
    public int chosenIndex;

    //Wird beim Start ausgeführt
    void Start()
    {
        //Holt sich alle gespeicherten KIs
        ais = AI.GetAll();

        //Eine Liste von Spalten
        items = new GameObject[ais.Count + 1];
        //Geht durch jede KI
        for (int i = 0; i < ais.Count + 1; i++)
        {
            //Es muss eine Kopie von i gemacht werden, da AddListener anscheinend asynchron verläuft
            int copyI = i;

            GameObject item;
        
            if (i == 0)
            {
                //Erstellt bei der KI Auswahl eine Spalte bei wlechem man eine neue erstellen kann
                item = Instantiate(aiNewPrefab, itemContainer.transform);

                item.transform.GetChild(1).GetComponent<InputField>().onEndEdit.AddListener((string input) => ChooseName(input));
            }

            else
            {
                //Erstellt für die momentane KI eine Spalte
                item = Instantiate(aiPrefab, itemContainer.transform);

                item.transform.GetChild(0).GetComponent<Text>().text = ais[i - 1].name;

                item.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => DeleteAI(copyI));
            }

            //Setzte die Aktion welche ausgeführt wird beim klicken auf die KI
            item.GetComponent<Button>().onClick.AddListener(() => ChooseAI(copyI));

            items[i] = item;
        }

        //Wähle standartmässig die erste Spalte
        items[0].GetComponent<Button>().Select();
    }

    //Wird ausgeführt wenn auf eine KI Spalte geklickt wird
    public void ChooseAI(int indexClicked)
    {
        chosenIndex = indexClicked;
        //Wähle die geklickte KI Spalte
        items[indexClicked].GetComponent<Button>().Select();

        if (indexClicked == 0)
        {
            //Deaktiviere die gewählte Spalte
            toogleTesting.GetComponent<Toggle>().interactable = false;
        }

        else
        {
            //Aktiviere die letzte gewählte Spalte
            toogleTesting.GetComponent<Toggle>().interactable = true;
        }
    }

    //Wähle denn neunen KI Namen
    public void ChooseName(string name)
    {
        newName = name;
    }

    //Lösche die ausgewählte KI
    public void DeleteAI(int indexClicked)
    {
        chosenIndex = indexClicked;
        ais[indexClicked - 1].Delete();

        Destroy(items[indexClicked]);
    }

    //Gehe zum nächsten Menu
    public void Next()
    {
        //Bei neuer KI erstelle diese
        if (chosenIndex == 0)
        {
            TrainingController.ai = new AI(newName);
            
        }

        else
        {
            TrainingController.ai = ais[chosenIndex - 1];

            //Falls man im Test Modus ist wird eine Variable gesetzt
            if (toogleTesting.GetComponent<Toggle>().isOn)
            {
                GeneticAlgorithm.testing = true;
            }

            else
            {
                GeneticAlgorithm.testing = false;
            }
        }

        //Lade die Szene des automatischen Parcours
        SceneManager.LoadScene((int)SceneNumber.PARCOUR_AUTO);
    }

    //gehe zurück im Menu
    public void Back()
    {
        SceneManager.LoadScene((int)SceneNumber.MENU_COURSE_SELECTION);
    }
}