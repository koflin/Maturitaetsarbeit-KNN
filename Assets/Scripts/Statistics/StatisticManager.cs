using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticManager : MonoBehaviour {

    public GameObject graphic;

	public void LoadOneStatistic(TrainingSession trainingSession, int statType)
    {
        Debug.Log("LOAD");
        string xLabel = "";
        string yLabel = "";
        IDictionary<double, double> stats = new Dictionary<double, double>();

        switch (statType)
        {
            //Average Fitness
            case 0:
                for(int i = 0; i < trainingSession.generations.Count; i++)
                {
                    xLabel = "Generation";
                    yLabel = "Fitness";
                    stats.Add(i + 1, trainingSession.generations[i].GetAverageIndividualFitness());
                }
                break;

            case 1:
                for(int i = 0; i < trainingSession.generations.Count; i++)
                {
                    xLabel = "Generation";
                    yLabel = "Fitness";
                    stats.Add(i + 1, trainingSession.generations[i].GetBestIndividualFitness());
                }
                break;

            case 2:
                for (int i = 0; i < trainingSession.generations.Count; i++)
                {
                    xLabel = "Generation";
                    yLabel = "Successrate (% of Population)";
                    stats.Add(i + 1, trainingSession.generations[i].GetSuccessRate() * 100);
                }
                break;
        }

        graphic.GetComponent<GraphicManager>().ShowData(xLabel, yLabel, stats);
    }

    public void LoadAllStatistics(List<TrainingSession> trainingSessions, int statType)
    {
        Debug.Log("LOAD");
        string xLabel = "";
        string yLabel = "";
        IDictionary<double, double> stats = new Dictionary<double, double>();

        switch (statType)
        {
            //Average Fitness
            case 0:
                xLabel = "Generation";
                yLabel = "Fitness";
                for (int i = 0; i < trainingSessions[0].generations.Count; i++)
                {
                    double value = 0;
                    foreach (TrainingSession trainingSession in trainingSessions)
                    {
                        value += trainingSession.generations[i].GetAverageIndividualFitness();
                    }
                    stats.Add(i + 1, value / trainingSessions.Count);
                }
                break;

            case 1:
                xLabel = "Generation";
                yLabel = "Fitness";
                for (int i = 0; i < trainingSessions[0].generations.Count; i++)
                {
                    double value = 0;
                    foreach (TrainingSession trainingSession in trainingSessions)
                    {
                        value += trainingSession.generations[i].GetBestIndividualFitness();
                    }
                    stats.Add(i + 1, value / trainingSessions.Count);
                }
                break;

            case 2:
                xLabel = "Generation";
                yLabel = "Successrate (% of Population)";
                for (int i = 0; i < trainingSessions[0].generations.Count; i++)
                {
                    double value = 0;
                    foreach (TrainingSession trainingSession in trainingSessions)
                    {
                        value += trainingSession.generations[i].GetSuccessRate() * 100;
                    }
                    stats.Add(i + 1, value / trainingSessions.Count);
                }
                break;
        }
        graphic.GetComponent<GraphicManager>().ShowData(xLabel, yLabel, stats);
    }
}
