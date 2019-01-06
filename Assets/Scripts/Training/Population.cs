using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Diese Klasse stellt eine Population dar
public class Population {

    //Liste der Inviduen der Population
    public List<Individual> individuals;

    //Gibt die KNNs der Inviduen zurück
    public List<NeuralNetwork> GetNNs()
    {

        List<NeuralNetwork> nns = new List<NeuralNetwork>();
        foreach (Individual individual in individuals)
        {
            nns.Add(individual.nn);
        }

        return nns;
    }

    public Population()
    {
        individuals = new List<Individual>();
    }

    public Population(List<Individual> individuals)
    {
        this.individuals = individuals;
    }
    
    //Ermittelt das Inviduum anhand seiner physischen Instanz
    public Individual GetIndividualByGameObject(GameObject gameObject)
    {
        foreach (Individual individual in individuals)
        {
            if (individual.GetGameObject() == gameObject)
            {
                return individual;
            }
        }

        return null;
    }

    //Prüfen ob alle Individuen zerstört wurden
    public bool HaveAllStopped()
    {
        foreach (Individual individual in individuals)
        {
            if (!individual.hasStopped)
            {
                return false;
            }
        }

        return true;
    }

    //Zurückgeben einer Liste von Eltern Individuen NN's
    public List<NeuralNetwork> Select()
    {
        List<NeuralNetwork> parents = new List<NeuralNetwork>();
        int populationFitness = ComputePopulationFitness();

        //Schleife welche Eltern auswählt, die Anzahl Eltern entspricht der Anzahl jetztigen Individuen
        for (int parentIndex = 0; parentIndex < individuals.Count; parentIndex++)
        {
            //Generiere eine zufällige Zahl von 1 bis zur Populationsfitness
            int randomNumber = Random.Range(1, populationFitness);
            //Setze die jetztige Nummer auf 0
            int currentNumber = 0;

            //Schleife durch die Individuen
            foreach (Individual individual in individuals)
            {
                //Addieren der jetztigen Numer mit der jetztigen Fitness
                currentNumber += individual.GetFitness();

                //Wenn die jetztige Nummer grösser oder gleich der zufälligen Nummer ist wird dieses Individuum als Elternteil ausgewählt
                if (currentNumber >= randomNumber)
                {
                    parents.Add(individual.nn);
                    break;
                }
            }
        }

        return parents;
    }

    //Zurückgeben des besten Individuums
    public Individual GetBestIndividual()
    {
        Individual best = individuals[0];

        for (int i = 1; i < individuals.Count; i++)
        {
            if (individuals[i].fitness > best.fitness)
            {
                best = individuals[i];
            }
        }

        return best;
    }

    //Berechnen der Fitness der gesamten Population
    private int ComputePopulationFitness()
    {
        int populationFitness = 0;

        //Schleife durch jedes Individuum der Population
        foreach (Individual individual in individuals)
        {
            //Addieren der Populations -und Individuumsfitness
            populationFitness += individual.GetFitness();   
        }

        return populationFitness;
    }
}
