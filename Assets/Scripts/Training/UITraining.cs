using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITraining : MonoBehaviour {

    [Header("Training")]
    public GameObject statUI;
    public Text statGeneration;
    public Text statBestFitness;
    public Text statDNA;
    public GameObject buttonStuck;
    public GameObject buttonBack;

    public void SetTrainingResults(int generations, int bestFitness, string stringDNA)
    {
        statGeneration.text = generations.ToString();
        statBestFitness.text = bestFitness.ToString();
        statDNA.text = stringDNA;
    }

    public void ShowTrainingResults()
    {
        statUI.SetActive(true);
    }

    public void HideTrainingResults()
    {
        statUI.SetActive(true);
    }

    public void ShowButtonStuck()
    {
        buttonStuck.SetActive(true);
    }

    public void HideButtonStuck()
    {
        buttonStuck.SetActive(false);
    }
}
