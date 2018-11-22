using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individual {

    public GameObject gameObject;
    public bool hasCrahsed;
 
    public double fitness = 0;
    public NeuralNetwork nn;

    public Individual(NeuralNetwork nn, GameObject gameObject)
    {
        this.nn = nn;
        this.gameObject = gameObject;
    }

    public int GetFitness()
    {
        return (int)fitness;
    }

    public void RoundFitness()
    {
        fitness = Mathf.RoundToInt((float)fitness);
    }
}
