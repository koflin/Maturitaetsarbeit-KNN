using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class TrainingSession {

    public string courseId;
    public Time created;

    public List<GenerationStorable> generations;

    //Konstruktor für die Desirialisierung
    public TrainingSession()
    {
        
    }

    //Erstmalige Instanziierung
    public TrainingSession(string courseId)
    {
        this.courseId = courseId;
        this.created = DateTime.Now;

        this.generations = new List<GenerationStorable>();
    }

    //Hinzufügen einer Generation
    public void AddGeneration(Population generation)
    {
        generations.Add(new GenerationStorable(generation));
    }

    //Gibt die letzte Generation zurück
    public GenerationStorable GetLastGeneration()
    {
        if (generations.Count > 0)
        {
            return generations[0];
        }

        return null;
    }
}
