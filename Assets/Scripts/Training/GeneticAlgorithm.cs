﻿using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GeneticAlgorithm : MonoBehaviour {

    [Header("NN Settings")]
    public int populationSize = 20;
    public List<int> neuronAmounts = new List<int> { 5, 4, 1};
    public double dilation = 1;
    public int mutationRate = 10; //Geht von 0 bis 1000 (Promille)
    public int maxGenerations = 10;

    [Header("Unity Settings")]
    public Tilemap path;
    public GameObject runnerPrefab;
    public Vector2 startingPosition;
    public bool running = false;

    [Header("NN Stats")]
    public int generation = 1;
    public Population population;

    [Header("UI")]
    public GameObject statUI;
    public Text statGeneration;
    public Text statBestFitness;
    public Text statDNA;

    //Wirt am Anfag der Szene aufgerufen
    public void Start()
    {
        GenerateRandomPopulation();

        running = true;
    }

    //Wird in einem bestimmten Interval aufgerufen
    public void FixedUpdate()
    {
        if (running)
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
    }

    #region Events
    //Wird aufgerufen wenn ein Individuum in die Lava fällt
    public void OnIndividualCrash(GameObject physicalIndividual)
    {
        //Herholen des virtuellen Individuums anhand der physischen Instanz
        Individual individual = population.GetIndividualByGameObject(physicalIndividual);

        //Setzte das Individuum auf zerstört
        individual.hasCrahsed = true;
        //Runde die Fitness des Individuums
        individual.RoundFitness();

        //Prüfen ob alle Individuen verunfallt sind
        if (population.HaveAllCrashed())
        {
            running = false;

            if (maxGenerations > generation)
            {
                //Weiterentwickeln der Population
                Evolve();

                Debug.Log("Evolving...");
            }

            //Wenn die maximale Generationenanzahl erreicht wurde (Ende)
            else
            {
                Finish();

                Debug.Log("Finished...");
            }
        }

        Debug.Log("Individual " + physicalIndividual.GetInstanceID() + " has finished with fitness " + individual.fitness);
    }

    //Wird aufgerufen wenn ein Individuum das Ziel erreicht hat
    public void OnIndividualEndReached(GameObject physicalIndividual)
    {
        running = false;

        if (maxGenerations > generation)
        {
            //Weiterentwickeln der Population
            Evolve();

            Debug.Log("Evolving...");
        }

        //Wenn die maximale Generationenanzahl erreicht wurde (Ende)
        else
        {
            Finish();

            Debug.Log("Finished...");
        }
    }
    #endregion

    //Evolvieren der Population
    public void Evolve()
    {
        //Löschen der alten Population
        DespawnPopulation();

        //Zurücksetzen der Kamera

        //Züchten

        //Selektion
        List<NeuralNetwork> parents = population.Select();
        List<NeuralNetwork> offSpring = new List<NeuralNetwork>();

        for (int parentIndex = 0; parentIndex < parents.Count / 2; parentIndex++)
        {
            //Crossover (Ein-Punkt-Crossover)

            //Setzte die Eltern DNA's
            List<double> dnaOne = parents[parentIndex * 2].DNA;
            List<double> dnaTwo = parents[parentIndex * 2 + 1].DNA;

            //Werfe eine System.Exception falls die zwei DNA Stücke nicht gleich lang sind
            if (dnaOne.Count != dnaTwo.Count)
            {
                throw new System.Exception("The two DNA strings didn't have the same length.");
            }

            //Zufälliger Crossover Punkt (Punkt 0 wäre nach dem ersten Gen)
            int crossoverPoint = Random.Range(0, dnaOne.Count - 1);

            //Inizialisiere leere neue DNA Stücke
            List<double> newDnaOne = new List<double>();
            List<double> newDnaTwo = new List<double>();

            //Setzte die neuen DNA Stücke aus den alten zusammen

            newDnaOne.AddRange(dnaOne.GetRange(0, crossoverPoint + 1));
            newDnaOne.AddRange(dnaTwo.GetRange(crossoverPoint + 1, dnaOne.Count - (crossoverPoint + 1)));

            newDnaTwo.AddRange(dnaTwo.GetRange(0, crossoverPoint + 1));
            newDnaTwo.AddRange(dnaOne.GetRange(crossoverPoint + 1, dnaOne.Count - (crossoverPoint + 1)));
            
            
            //Mutation

            //Schleife durch jedes Gen
            for (int genIndex = 0; genIndex < newDnaOne.Count; genIndex++)
            {
                //Zufällige werte von 1 bis und mit 1000
                int randomOne = Random.Range(1, 1001);
                int randomTwo = Random.Range(1, 1001);

                //Mutiert wenn der zufällige Wert im Mutationsbereich liegt
                if (randomOne <= mutationRate)
                {
                    //Generiere eine float Zahl die von -5 bis und mit 5 geht
                    newDnaOne[genIndex] = Random.Range(-5f, 5f);
                }

                //Mutiert wenn der zufällige Wert im Mutationsbereich liegt
                if (randomTwo <= mutationRate)
                {
                    //Generiere eine float Zahl die von -5 bis und mit 5 geht
                    newDnaTwo[genIndex] = Random.Range(-5f, 5f);
                }
            }

            offSpring.Add(new NeuralNetwork(neuronAmounts, newDnaOne));
            offSpring.Add(new NeuralNetwork(neuronAmounts, newDnaTwo));
        }

        //Ersetzen (Nachwuchs ersetzt alte Population)

        GeneratePopulation(offSpring);

        running = true;
        generation += 1;
    }

    //Beenden des Trainings
    public void Finish()
    {
        //Löschen der alten Population
        DespawnPopulation();

        ShowStats();
    }

    //Generierung einer zufälligen Population
    private void GenerateRandomPopulation()
    {
        population = new Population();
        List<GameObject> physicalPopulation = SpawnPopulation();

        for (int i = 0; i < populationSize; i++)
        {
            //Virtuelle Instanzierung des Individuums
            population.individuals.Add(new Individual(new NeuralNetwork(neuronAmounts, CalcDNALength()), physicalPopulation[i]));
        }
    }

    //Generierung einer definierten Population
    private void GeneratePopulation(List<NeuralNetwork> nns)
    {
        population = new Population();
        List<GameObject> physicalPopulation = SpawnPopulation();

        for (int i = 0; i < populationSize; i++)
        {
            //Virtuelle Instanzierung des Individuums
            population.individuals.Add(new Individual(nns[i], physicalPopulation[i]));
        }
    }

    //Erstellt alle physischen Individuen
    private List<GameObject> SpawnPopulation()
    {
        List<GameObject> physicalPopulation = new List<GameObject>();

        //Schleife für jedes Individuum der Population
        for (int i = 0; i < populationSize; i++)
        {
            //Physische Instanziierung des Individuums
            GameObject physicalIndividual = Instantiate(runnerPrefab, startingPosition, Quaternion.identity);
         
            CharController charController = physicalIndividual.GetComponent<CharController>();
            //Übergeben des GenetischenAlgorithmuses
            charController.geneticAlgorithm = this;
            //Setzen des Spielfeldes
            charController.tilemap = path;
            //Wechseln auf automatische Steuerung durch KI
            charController.manual = false;

            physicalPopulation.Add(physicalIndividual);
        }

        return physicalPopulation;
    }

    //Löscht alle physischen Individuen
    private void DespawnPopulation()
    {
        //Loop durch alle physischen Individuen
        foreach (Individual individual in population.individuals)
        {
            //Zerstören eines einzelnen Individuums
            Destroy(individual.gameObject);
        }
    }

    //Berechnet die Fitness mithilfe der Distanz Inputs
    private double ComputeFitness(List<double> distances)
    {
        double average = 0;

        //Aufsummieren aller Distanzen
        foreach (double distance in distances)
        {
            average += distance;
        }

        //Teilen durch die Anzahl der Distanzen
        average /= distances.Count;

        //Zurückgeben des skalierten Durchschnittes
        return average * dilation;
    }

    //Berechnet die länge der DNA
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

    //Zeigt alle Statistiken am Schluss
    private void ShowStats()
    {
        //Wählen des besten Individuums
        Individual bestIndividual = population.GetBestIndividual();

        //Anzeigen der Resultate
        statGeneration.text = (generation).ToString();
        statBestFitness.text = bestIndividual.GetFitness().ToString();
        statDNA.text = bestIndividual.nn.GetRoundedDNAString();

        statUI.SetActive(true);
    }
}