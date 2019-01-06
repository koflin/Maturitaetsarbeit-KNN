using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Diese Klasse stellt ein Individuum dar
public class Individual {

    private GameObject gameObject;
    public bool hasStopped = false;
    public bool hasFinished = false;
 
    public double fitness = 1;
    public NeuralNetwork nn;

    public Individual()
    {

    }

    public Individual(NeuralNetwork nn, GameObject gameObject)
    {
        this.nn = nn;
        this.gameObject = gameObject;
    }

    //Ermittelt ob das Individuum fertig ist
    public bool HasFinished()
    {
        return hasFinished;
    }

    //Gibt das physische Individuum zurück
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    //Gibt die Fitness zurück
    public int GetFitness()
    {
        return (int)fitness;
    }

    //Rundet die Fitness des Inviduums
    public void RoundFitness()
    {
        fitness = Mathf.RoundToInt((float)fitness);
    }
}
