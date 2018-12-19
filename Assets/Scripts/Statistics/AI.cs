using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class AI  {

    public string id;
    public string name;
    public Time created;

    public List<TrainingSession> trainingSessions;

    //Konstruktor für die Desirialisierung
    public AI()
    {
    }

    //Neu instanziierung
    public AI(string name)
    {
        this.id = Guid.NewGuid().ToString();

        if (String.IsNullOrEmpty(name))
        {
            name = "Unnamed";
        }

        this.name = name;
        this.created = DateTime.Now;

        this.trainingSessions = new List<TrainingSession>();
    }

    public List<NeuralNetwork> GetNNs()
    {
        TrainingSession latestTrainingSession = trainingSessions[trainingSessions.Count - 1];
        GenerationStorable latestPopulation = latestTrainingSession.generations[latestTrainingSession.generations.Count - 1];
        List<NeuralNetwork> nns = new List<NeuralNetwork>();

        foreach (IndividualStorable individual in latestPopulation.individuals)
        {
            nns.Add(new NeuralNetwork(individual.neuronAmounts, individual.dna));
        }

        return nns;
    }

    //Zurückgeben der besten TrainingsSession auf dem ausgewählten course
    public TrainingSession GetBestSessionByCourse(string courseId)
    {
        List<TrainingSession> sessionsOnCourse = new List<TrainingSession>();

        foreach(TrainingSession session in trainingSessions)
        {
            if (session.courseId == courseId)
            {
                sessionsOnCourse.Add(session);
            }
        }

        if (sessionsOnCourse.Count != 0)
        {
            TrainingSession bestTrainingSession = sessionsOnCourse[0];
            int bestFitness = sessionsOnCourse[0].GetLastGeneration().GetBestIndividualFitness();

            for (int i = 1; i < sessionsOnCourse.Count; i++)
            {
                int fitness = sessionsOnCourse[i].GetLastGeneration().GetBestIndividualFitness();
                if (fitness > bestFitness)
                {
                    bestTrainingSession = sessionsOnCourse[i];
                    bestFitness = fitness;
                }
            }

            return bestTrainingSession;
        }

        return null;
    }

    //Hinzufügen einer Session
    public void AddSession(TrainingSession trainingSession)
    {
        trainingSessions.Add(trainingSession);
    }

    //Gibt die letzte Trainings Session zurück
    public TrainingSession GetLatestTrainingSession()
    {
        for (int i = trainingSessions.Count - 1; i >= 0; i--)
        {
            if (trainingSessions[i].generations.Count > 1)
            {
                return trainingSessions[i];
            }
        }
        return null;
    }

    //Speichern der KI im Speicher
    public void Store()
    {
        string jsonObject = JsonUtility.ToJson(this);
        StreamWriter writer;

        writer = new StreamWriter(Application.streamingAssetsPath + "/AIs/" + id + ".json");

        writer.Write(jsonObject);
        writer.Close();
    }

    //Löschen der KI im Speicher
    public void Delete()
    {
        if (File.Exists(Application.streamingAssetsPath + "/AIs/" + id + ".json"))
        {
            File.Delete(Application.streamingAssetsPath + "/AIs/" + id + ".json");
            File.Delete(Application.streamingAssetsPath + "/AIs/" + id + ".json.meta");
            return;
        }

        throw new NullReferenceException("AI could not be deleted because it was not found");
    }

    //Instanziierung aller AIs vom Speicher
    public static List<AI> GetAll()
    {
        List<AI> aiList = new List<AI>();
        string[] files = Directory.GetFiles(Application.streamingAssetsPath + "/AIs");

        foreach (string fileName in files)
        {
            if (Path.GetExtension(Application.streamingAssetsPath + "/AIs" + fileName) == ".json")
            {
                StreamReader reader = new StreamReader(fileName);
                string fileText = reader.ReadToEnd();
                reader.Close();

                AI ai = JsonUtility.FromJson<AI>(fileText);

                if (ai != null)
                {
                    aiList.Add(ai);
                }
            }
        }

        return aiList;
    }
}
