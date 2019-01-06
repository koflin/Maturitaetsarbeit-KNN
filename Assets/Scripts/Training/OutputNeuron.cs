using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Diese Klasse stellt ein Ausgabe Neuron dar
public class OutputNeuron : Neuron
{
    public OutputNeuron(List<double> weights, double bias) : base(weights, bias)
    {

    }

    //In diesem Fall nehme ich die Softsign Funktion, da ich einen Wert der von -1 bis 1 geht haben will
    public override double ApplyActivasion(double netInput)
    {
        return netInput / (1 + Math.Abs(netInput));
    }
}
