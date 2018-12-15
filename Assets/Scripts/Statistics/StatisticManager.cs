using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticManager : MonoBehaviour {

    public GameObject graphic;

	public void LoadStatistic(AI ai, TrainingSession trainingSession, int statType)
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
                    stats.Add(i + 1, trainingSession.generations[i].GetSuccesRate() * 100);
                }
                break;
        }

        graphic.GetComponent<GraphicManager>().ShowData(xLabel, yLabel, stats);
    }
}
