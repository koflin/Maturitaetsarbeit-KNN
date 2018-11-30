using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Neuron {

    /*
     * Currently out of use
     */

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

    //Berechnung der Aktivierung
    public abstract double ApplyActivasion(double netInput);
}
