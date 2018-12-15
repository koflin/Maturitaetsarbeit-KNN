﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool HasFinished()
    {
        return hasFinished;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
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
