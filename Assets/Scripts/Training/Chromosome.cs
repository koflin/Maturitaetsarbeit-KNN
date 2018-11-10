using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome {

	public double[] DNA;

    public Chromosome(int size)
    {
        DNA = new double[size];

        for (int gene = 0; gene < size; gene++)
        {
            //Jedes Gen ist eine zufällige Zahl zwischen -5 und 5
            DNA[gene] = Random.Range(-5f, 5f);
        }
    }

    public Chromosome(double[] DNA)
    {
        this.DNA = DNA;
    }
}
