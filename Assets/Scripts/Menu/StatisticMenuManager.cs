using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MenuManager;

//Diese Klasse steuert das Statistik Menu
public class StatisticMenuManager : MonoBehaviour {

    public GameObject modeSelection;
    public GameObject aiSelection;
    public GameObject sessionSelection;
    public GameObject criteriaSelection;

    public bool onlyOneAI = true;
    public StatisticManager statisticManager;

    List<AI> ais;

    //Wird beim Start ausgeführt
    private void Start()
    {
        statisticManager = GetComponent<StatisticManager>();

        //Hole jede KI
        ais = AI.GetAll();

        List<Dropdown.OptionData> aiOptions = new List<Dropdown.OptionData>();
        foreach (AI ai in ais)
        {
            //Generiere eine Dropdown Option für jede KI
            aiOptions.Add(new Dropdown.OptionData(ai.name));
        }

        //Generiere eine Dropdown Option für die Modi "One AI" und "All AI"
        modeSelection.GetComponent<Dropdown>().AddOptions(new List<Dropdown.OptionData>() { new Dropdown.OptionData("One AI"), new Dropdown.OptionData("All AIs") });

        aiSelection.GetComponent<Dropdown>().AddOptions(aiOptions);

        //Generiere für jeden Statistik Modi eine Dropdown Option
        criteriaSelection.GetComponent<Dropdown>().AddOptions(new List<Dropdown.OptionData>() { new Dropdown.OptionData("Average Fitness"), new Dropdown.OptionData("Best Fitness"), new Dropdown.OptionData("Successrate") });

        //Aktualsiere die Session Dropdown Optionen
        RefreshSessionOption(0);
    }

    //Lade Statistiken
    public void LoadStatistics()
    {
        if (onlyOneAI)
        {
            AI ai = ais[aiSelection.GetComponent<Dropdown>().value];
            TrainingSession session = ai.trainingSessions[sessionSelection.GetComponent<Dropdown>().value];
            int criteria = criteriaSelection.GetComponent<Dropdown>().value;

            statisticManager.LoadOneStatistic(session, criteria);
        }

        else
        {
            List<AI> ais = AI.GetAll();
            List<TrainingSession> sessions = new List<TrainingSession>();

            foreach (AI ai in ais)
            {
                sessions.Add(ai.GetLatestSession());
            }

            statisticManager.LoadAllStatistics(sessions, criteriaSelection.GetComponent<Dropdown>().value);
        }
    }

    public void RefreshOptions(int newModeOption)
    {
        //Wenn der Modus "One AI" genommen wird werden noch Session und KI Optionen aktiviert
        if (newModeOption == 0)
        {
            onlyOneAI = true;
            aiSelection.SetActive(true);
            sessionSelection.SetActive(true);
        }

        else
        {
            onlyOneAI = false;
            aiSelection.SetActive(false);
            sessionSelection.SetActive(false);
        }
    }

    //Jede KI hat andere Session, lade diese im Menu
    public void RefreshSessionOption(int newAIOption)
    {
        sessionSelection.GetComponent<Dropdown>().ClearOptions();

        List<Dropdown.OptionData> sessionOptions = new List<Dropdown.OptionData>();
        foreach (TrainingSession session in ais[newAIOption].trainingSessions)
        {
            sessionOptions.Add(new Dropdown.OptionData(((DateTime)session.created).ToString("dd.MM.yyyy HH:mm:ss")));
        }

        sessionSelection.GetComponent<Dropdown>().AddOptions(sessionOptions);
    }

    //Gehe zum Hauptmenu zurück
    public void Back()
    {
        SceneManager.LoadScene((int)SceneNumber.MENU_MAIN);
    }
}
