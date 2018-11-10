using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneticAlgorithm : MonoBehaviour {

    [Header("NN Settings")]
    public int populationSize = 20;
    public List<int> neuronAmounts = new List<int> { 5, 4, 1};
    public double dilation = 1;

    [Header("Unity Settings")]
    public Tilemap path;
    public GameObject runnerPrefab;
    public Vector2 startingPosition;


    public int generation = 1;
    public Population population;

    public void Start()
    {
        InitializePopulation();
    }

    //Wird in einem bestimmten Interval aufgerufen
    public void FixedUpdate()
    {
        int counter = 1;
        foreach (Individual individual in population.individuals)
        {
            NeuralNetwork nn = individual.nn;
            CharController cc = individual.gameObject.GetComponent<CharController>();
            List<double> distances = cc.CalculateDistances();

            if (!cc.IsCrashed())
            {
                cc.Move((float)nn.ComputeOutput(distances));
                individual.fitness += ComputeFitness(cc.CalculateDistances());
            }

            counter += 1;
        }
    }

    public void OnIndividualCrash(GameObject physicalIndividual)
    {
        Individual individual = population.GetIndividualByGameObject(physicalIndividual);

        Debug.Log("Individual " + physicalIndividual.GetInstanceID() + " has finished with fitness " + individual.fitness);
    }

    public void InitializePopulation()
    {
        population = new Population();

        for (int i = 0; i < populationSize; i++)
        {
            //Physische Inizialisierung des Individuums
            GameObject physicalIndividual = Instantiate(runnerPrefab, startingPosition, Quaternion.identity);
            CharController charController = physicalIndividual.GetComponent<CharController>();
            charController.geneticAlgorithm = this;
            charController.tilemap = path;
            charController.manual = false;

            //Virtuelle Inizialisierung des Individuums
            population.individuals.Add(new Individual(new NeuralNetwork(neuronAmounts, CalcDNALength()), physicalIndividual));
        }
    }

    public double ComputeFitness(List<double> distances)
    {
        //Berechnung des Durchschnitts
        double average = 0;
        foreach (double distance in distances)
        {
            average += distance;
        }
        average /= distances.Count;

        return Mathf.Round((float)(average * dilation));
    }


    private int CalcDNALength()
    {
        //Die Input Neuronen
        int length = 0;

        for (int i = 1; i < neuronAmounts.Count; i++)
        {
            //Slot für die Neuronen
            length += neuronAmounts[i - 1] * neuronAmounts[i];
        }

        for (int i = 1; i < neuronAmounts.Count; i++)
        {
            //Slot für den Bias
            length += neuronAmounts[i];
        }

        return length;
    }
}
