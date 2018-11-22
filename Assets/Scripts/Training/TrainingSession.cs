using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TrainingSession {

    public static string storagePath = Application.streamingAssetsPath + "/Sessions";

    public string id;
    public string name;
    public DateTime created;

    public int populationSize;
    public int generationSize;
    public List<Population> populations;

    //Erstmalige Instanziierung
    public TrainingSession(string name, int populationSize, int generationSize)
    {
        this.id = Guid.NewGuid().ToString();

        if (String.IsNullOrEmpty(name))
        {
            name = "";
        }

        this.name = name;
        this.created = DateTime.Now;

        this.populationSize = populationSize;
        this.generationSize = generationSize;
        this.populations = new List<Population>();
    }

    //Instanziierung vom Speicher
    public TrainingSession(string id)
    {
        StreamReader reader = new StreamReader(storagePath + "/" + id + ".txt");
        string fileText = reader.ReadToEnd();
        reader.Close();

        TrainingSession session = JsonMapper.ToObject<TrainingSession>(fileText);

        this.id = id;
        this.name = session.name;
        this.created = session.created;

        this.populationSize = session.populationSize;
        this.generationSize = session.generationSize;
        this.populations = session.populations;
    }

    //Hinzufügen einer Generation
    public void AddGeneration(Population population)
    {
        if (populationSize == populations.Count)
        {
            throw new IndexOutOfRangeException("Predefined population size reached!");
        }

        populations.Add(population);
    }

    //Speichern der Session im Speicher
    public void Store()
    {
        string jsonObject = JsonMapper.ToJson(this);
        StreamWriter writer;

        writer = new StreamWriter(storagePath + "/" + id + ".txt");

        writer.Write(jsonObject);
        writer.Close();
    }

    //Instanziierung aller Sessions vom Speicher
    public static List<TrainingSession> GetAll()
    {
        List<TrainingSession> trainingSessions = new List<TrainingSession>();
        string[] files = Directory.GetFiles(storagePath);

        foreach (string fileName in files)
        {
            StreamReader reader = new StreamReader(storagePath + "/" + fileName);
            string fileText = reader.ReadToEnd();
            reader.Close();

            TrainingSession session = JsonMapper.ToObject<TrainingSession>(fileText);

            if (session != null)
            {
                trainingSessions.Add(session);
            }
        }

        return trainingSessions;
    }
}
