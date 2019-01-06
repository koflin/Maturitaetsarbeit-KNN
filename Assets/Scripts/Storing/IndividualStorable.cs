using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Diese Klasse ist ein Speicherbehälter eines Individuums
[Serializable]
public class IndividualStorable {

    public List<int> neuronAmounts;
    public List<double> dna;
    public int fitness;
    public bool finished;

    public IndividualStorable()
    {

    }

    public IndividualStorable(Individual individual)
    {
        this.neuronAmounts = individual.nn.neuronAmounts;
        this.dna = individual.nn.GetDNA();
        this.fitness = individual.GetFitness();
        this.finished = individual.HasFinished();
    }
}
