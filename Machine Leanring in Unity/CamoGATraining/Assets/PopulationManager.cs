using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{

    // Link to the person prefab
    public GameObject personPrefab;
    public int populationSize = 10;

    // Keep track of the people we create using a list
    List<GameObject> population = new List<GameObject>();

    // Timer
    public static float elapsed = 0;

    // Code to begin breeding
    int trialTime = 10;

    // Keep track of the generation
    int generation = 1;

    // sets a GUI to track some of the data we've created
    GUIStyle guiStyle = new GUIStyle();
    private void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int)elapsed, guiStyle);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Will loop 10 times to work out a random position on the screen
        for (int i = 0; i < populationSize; i++) 
        {
            // Screen parameters (Random.Range(x, y, z) note z = 0 since we are working in 2D
            // The Range is captured by moving the character on screen and taking the x, y, z location values - your screen's game view might be different!
            Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);

            // Creates the person and instantiates it, the Quaternion gives us a zero rotational value default
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);

            // Set the person's DNA by giving them a random colour value each between 0 - 1 (1 = white, 0 = black)
            go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);

            // New GameObject is added to the population list
            population.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update our timer
        elapsed += Time.deltaTime;
        if(elapsed > trialTime)
        {
            // New method runs to find out which memebers of hte "tribe" are the most fittest and then we breed them together, reset and cycle through for 10 more seconds
            BreedNewPopulation();
            elapsed = 0;
        }
    }
}
