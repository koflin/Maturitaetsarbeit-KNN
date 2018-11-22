using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer {

    public List<Neuron> neurons;

	public Layer()
    {
        this.neurons = new List<Neuron>();
    }

    //Ouput berechnen
    public List<double> ComputeOutputs(List<double> inputs)
    {
        List<double> outputs = new List<double>();

        //Loop durch jedes Neuron des Layers
        foreach(Neuron neuron in neurons)
        {
            //Berechnen des Outputs des Neurons
            outputs.Add(neuron.ComputeOutput(inputs));
        }

        return outputs;
    }
}
