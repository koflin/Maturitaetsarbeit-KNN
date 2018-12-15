using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GenerationStorable {

    public List<IndividualStorable> individuals;

    public GenerationStorable() { }

    public GenerationStorable(Population generation)
    {
        this.individuals = new List<IndividualStorable>();

        foreach (Individual individual in generation.individuals)
        {
            this.individuals.Add(new IndividualStorable(individual));
        }
    }

    public int GetBestIndividualFitness()
    {
        int bestFitness = individuals[0].fitness;

        for (int i = 1; i < individuals.Count; i++)
        {
            if (individuals[i].fitness > bestFitness)
            {
                bestFitness = individuals[i].fitness;
            }
        }

        return bestFitness;
    }

    public int GetGenerationFitness()
    {
        int generationFitness = 0;

        foreach (IndividualStorable individual in individuals)
        {
            generationFitness += individual.fitness;
        }

        return generationFitness;
    }

    public int GetAverageIndividualFitness()
    {
        float totalIndividualFitness = 0;

        foreach (IndividualStorable individual in individuals)
        {
            totalIndividualFitness += individual.fitness;
        }

        totalIndividualFitness /= individuals.Count;

        return Mathf.RoundToInt(totalIndividualFitness);
    }

    internal double GetSuccesRate()
    {
        int finished = 0;

        foreach (IndividualStorable individual in individuals)
        {
            if (individual.finished)
            {
                finished += 1;
            }
        }

        return finished / (double)individuals.Count;
    }
}
