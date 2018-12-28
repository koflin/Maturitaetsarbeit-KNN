using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MenuManager;

public class StatisticMenuManager : MonoBehaviour {

    public GameObject modeSelection;
    public GameObject aiSelection;
    public GameObject sessionSelection;
    public GameObject criteriaSelection;

    public bool onlyOneAI = true;
    public StatisticManager statisticManager;

    List<AI> ais;

    private void Start()
    {
        statisticManager = GetComponent<StatisticManager>();

        ais = AI.GetAll();

        List<Dropdown.OptionData> aiOptions = new List<Dropdown.OptionData>();
        foreach (AI ai in ais)
        {
            aiOptions.Add(new Dropdown.OptionData(ai.name));
        }

        modeSelection.GetComponent<Dropdown>().AddOptions(new List<Dropdown.OptionData>() { new Dropdown.OptionData("One AI"), new Dropdown.OptionData("All AIs") });

        aiSelection.GetComponent<Dropdown>().AddOptions(aiOptions);

        criteriaSelection.GetComponent<Dropdown>().AddOptions(new List<Dropdown.OptionData>() { new Dropdown.OptionData("Average Fitness"), new Dropdown.OptionData("Best Fitness"), new Dropdown.OptionData("Successrate") });

        RefreshSessionOption(0);
    }

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

    public void Back()
    {
        SceneManager.LoadScene((int)SceneNumber.MENU_MAIN);
    }
}
