using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Neuron {

    public List<double> weights;
    public double bias;

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
