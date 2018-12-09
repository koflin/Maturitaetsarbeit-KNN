using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MenuManager;

public class TrainingUI : MonoBehaviour {

    [Header("Training")]
    public GameObject statUI;
    public Text statGeneration;
    public Text statBestFitness;
    public Text statDNA;
    public GameObject buttonStuck;
    public GameObject buttonQuit;

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

    public void ShowButtonQuit()
    {
        buttonQuit.SetActive(true);
    }

    public void HideButtonQuit()
    {
        buttonQuit.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene((int)SceneNumber.MENU_MAIN);
    }
}
