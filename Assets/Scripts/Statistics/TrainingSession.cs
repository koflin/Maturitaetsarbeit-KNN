using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TrainingSession {

    public string courseId;
    public DateTime created;

    public List<Population> generations;

    //Konstruktor für die Desirialisierung
    public TrainingSession()
    {

    }

    //Erstmalige Instanziierung
    public TrainingSession(string courseId)
    {
        this.courseId = courseId;
        this.created = DateTime.Now;

        this.generations = new List<Population>();
    }

    //Hinzufügen einer Generation
    public void AddGeneration(Population generation)
    {
        generations.Add(generation);
    }

    //Gibt die letzte Generation zurück
    public Population GetLastGeneration()
    {
        if (generations.Count > 0)
        {
            return generations[0];
        }

        return null;
    }
}
