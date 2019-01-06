using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Diese Klasse stellt ein Neuron dar
public abstract class Neuron {

    public List<double> weights;
    public double bias;

    //Konstruktor für die Desirialisierung
    public Neuron()
    {

    }

    //Konstruktor
    public Neuron(List<double> weights, double bias)
    {
        this.weights = weights;
        this.bias = bias;
    }

    //Berechnung des Outputs
    public double ComputeOutput(List<double> inputs)
    {
        double netInput = bias;

        //Berechnung der "rohen" Netzeingabe
        for (int inputIndex = 0; inputIndex < inputs.Count; inputIndex++)
        {
            netInput += inputs[inputIndex] * weights[inputIndex];
        }

        return ApplyActivasion(netInput);
    }

    //Berechnung der Aktivierung (ist je nach Typ Neuron unterschiedlich)
    public abstract double ApplyActivasion(double netInput);
}
