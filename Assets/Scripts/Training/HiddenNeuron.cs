using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Diese Klasse stellt ein Verstecktes Neuron dar
public class HiddenNeuron : Neuron {

    public HiddenNeuron(List<double> weights, double bias) : base(weights, bias)
    {

    }

    //In diesem Fall nehme ich de ReLU (Rectified linear unit) Funktion
    public override double ApplyActivasion(double netInput)
    {
        if (netInput >= 0)
        {
            return netInput; 
        }

        return 0;
    }
}
