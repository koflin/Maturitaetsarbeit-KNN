using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individual {

    public GameObject gameObject;
 
    public double fitness = 0;
    public NeuralNetwork nn;

    public Individual(NeuralNetwork nn, GameObject gameObject)
    {
        this.nn = nn;
        this.gameObject = gameObject;
    }
}
