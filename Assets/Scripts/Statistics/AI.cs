using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AI  {

    public static string storagePath = Application.streamingAssetsPath + "/AIs";

    public string id;
    public string name;
    public DateTime created;

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
            int bestFitness = sessionsOnCourse[0].GetLastGeneration().GetBestIndividual().GetFitness();

            for (int i = 1; i < sessionsOnCourse.Count; i++)
            {
                int fitness = sessionsOnCourse[i].GetLastGeneration().GetBestIndividual().GetFitness();
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
    public TrainingSession GetLatestSession()
    {
        return trainingSessions[trainingSessions.Count - 1];
    }

    //Speichern der AI im Speicher
    public void Store()
    {
        string jsonObject = JsonMapper.ToJson(this);
        StreamWriter writer;

        writer = new StreamWriter(storagePath + "/" + id + ".txt");

        writer.Write(jsonObject);
        writer.Close();
    }

    //Instanziierung aller AIs vom Speicher
    public static List<AI> GetAll()
    {
        List<AI> aiList = new List<AI>();
        string[] files = Directory.GetFiles(storagePath);

        foreach (string fileName in files)
        {
            StreamReader reader = new StreamReader(fileName);
            string fileText = reader.ReadToEnd();
            reader.Close();

            AI ai = JsonMapper.ToObject<AI>(fileText);

            if (ai != null)
            {
                aiList.Add(ai);
            }
        }

        return aiList;
    }
}
