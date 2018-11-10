using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population {

    public List<Individual> individuals;

    public Population()
    {
        individuals = new List<Individual>();
    }

    public Population(List<Individual> individuals)
    {
        this.individuals = individuals;
    }
    
    public Individual GetIndividualByGameObject(GameObject gameObject)
    {
        foreach (Individual individual in individuals)
        {
            if (individual.gameObject == gameObject)
            {
                return individual;
            }
        }

        return null;
    }
}
