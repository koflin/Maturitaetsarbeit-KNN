using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Diese Klasse ist ein Speicherbehälter einer Generation
[Serializable]
public class GenerationStorable {
    //Liste der Individuen
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

    //Gibt die beste Fitness der Generation zurück
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

    //Gibt die gesamte Fitness aler Individduen zurück
    public int GetGenerationFitness()
    {
        int generationFitness = 0;

        foreach (IndividualStorable individual in individuals)
        {
            generationFitness += individual.fitness;
        }

        return generationFitness;
    }

    //Gibt die durchschnittliche Fitness der Individuen zurück
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

    //Gibt die Erfolgsrate der Generation zurück
    internal double GetSuccessRate()
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
