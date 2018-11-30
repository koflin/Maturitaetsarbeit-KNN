using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NeuralNetwork {

    public List<int> neuronAmounts;

    /*
     * DNA sieht wie folgt aus: Gewicht1.1 + Gewicht1.2 + Gewicht1.3 + Bias1 + Gewicht2.1 + Gewicht2.2 + Gewicht2.3 + Gewicht2.4 + Bias2 + .....
     */

    public List<double> DNA;

    //Der input Layer ist hier nicht enhalten!
    public List<Layer> layers = new List<Layer>();

    //Zurückgeben des DNA Strangs
    public List<double> GetDNA()
    {
        return DNA;
    }

    //Zurückgeben des gerundeten DNA Strangs
    public string GetRoundedDNAString()
    {
        List<double> roundedDNA = new List<double>();

        foreach (double gene in DNA)
        {
            roundedDNA.Add(Mathf.Round((float)(gene * 100)) / 100);
        }

        return JsonMapper.ToJson(roundedDNA);
    }

    //Konstruktor für die Desirialisierung
    public NeuralNetwork()
    {

    }

    //Konstruktor des NN mit vorgegebner DNA
    public NeuralNetwork(List<int> neuronAmounts, List<double> dna)
    {
        this.neuronAmounts = neuronAmounts;
        this.DNA = dna;

        InitializeNNFromDNA();
    }

    //Konstruktor des NN mit zufälliger DNA
    public NeuralNetwork(List<int> neuronAmounts, int size)
    {
        this.neuronAmounts = neuronAmounts;
        DNA = new List<double>();

        for (int gene = 0; gene < size; gene++)
        {
            //Jedes Gen ist eine zufällige Zahl zwischen -5 und 5
            DNA.Add(Random.Range(-5f, 5f));
        }

        InitializeNNFromDNA();
    }

    //Setzten der Gewichte und Biases mithilfe der DNA
    private void InitializeNNFromDNA()
    {
        int currentGenIndex = 0;

        //Loop durch jeden Layer ausser dem Input Layer
        for(int layerIndex = 1; layerIndex < neuronAmounts.Count; layerIndex++)
        {
            Layer layer = new Layer();

            //Loop durche jedes Neuron in dem jeweiligen Layer
            for (int neuronIndex = 0; neuronIndex < neuronAmounts[layerIndex]; neuronIndex++)
            {
                List<double> weights = new List<double>();

                //Loop durch jeden Genabschnitt (Gewichte und Biases)
                for (int genIndex = 0; genIndex < neuronAmounts[layerIndex - 1]; genIndex++)
                {
                    //Das Gewicht entspricht der Information des Gens an der Stelle currentByteIndex
                    weights.Add(DNA[currentGenIndex]);

                    currentGenIndex += 1;
                }

                //Ouput Neuronen
                if (layerIndex == neuronAmounts.Count - 1)
                {
                    layer.neurons.Add(new OutputNeuron(weights, DNA[currentGenIndex]));
                }

                //Hidden Neuronen
                else
                {
                    layer.neurons.Add(new HiddenNeuron(weights, DNA[currentGenIndex]));
                }

                //Der Bias zählt auch als Gen, darum wird der genIndex erhöht
                currentGenIndex += 1;
            }

            layers.Add(layer);
        }
    }

    //Berechnung des Ouputs
    public double ComputeOutput(List<double> inputs)
    {
        List<double> outputs = inputs;

        //Loop durch alle Layers
        for(int layerIndex = 0; layerIndex < layers.Count; layerIndex++)
        {
            //Der momentane layeroutput wird mithilfe des vorherigen layerouputs berechnet
            outputs = layers[layerIndex].ComputeOutputs(outputs);
        }

        //Der letzte Ouput wird nur aus einer Liste mit der Länge 1 bestehen, da es nur ein OuputNeuron gibt
        return outputs[0];
    }

    //Debug Funktion welche das NN Objekt als JSON String in dem Unity Editor ausgibt
    public void DebugNN()
    {
        string objectString = JsonMapper.ToJson(this);
        Debug.Log(objectString);
    }
}
