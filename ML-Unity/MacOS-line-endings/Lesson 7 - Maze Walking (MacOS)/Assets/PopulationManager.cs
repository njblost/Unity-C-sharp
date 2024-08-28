using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MazeWalking {

    public class PopulationManager : MonoBehaviour {

        public GameObject botPrefab;
        public GameObject startPos;
        public int populationSize = 50;
        List<GameObject> population = new List<GameObject>();
        public static float elapsed = 0.0f;
        public float trialTime = 10.0f;
        private int generation = 1;

        GUIStyle guiStyle = new GUIStyle();

        private void OnGUI() {

            guiStyle.fontSize = 25;
            guiStyle.normal.textColor = Color.white;
            GUI.BeginGroup(new Rect(10, 10, 250, 250));
            GUI.Box(new Rect(0, 0, 140, 140), "Stats", guiStyle);
            GUI.Label(new Rect(10, 25, 200, 30), "Gen: " + generation, guiStyle);
            GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time: {0:0.00}", elapsed), guiStyle);
            GUI.Label(new Rect(10, 75, 200, 30), "Population" + population.Count, guiStyle);
            GUI.EndGroup();
        }

        void Start() {

            for (int i = 0; i < populationSize; ++i) {

                GameObject b = Instantiate(botPrefab, startPos.transform.position, this.transform.rotation);
                b.GetComponent<Brain>().Init();
                population.Add(b);
            }
        }

        GameObject Breed(GameObject parent1, GameObject parent2) {

            GameObject offspring = Instantiate(
                botPrefab,
                startPos.transform.position
                , this.transform.rotation);

            Brain b = offspring.GetComponent<Brain>();

            if (Random.Range(0, 100) == 1) { // Mutate 1 in 100

                b.Init();
                b.dna.Mutate();
            }
            else {

                b.Init();
                b.dna.Combine(parent1.GetComponent<Brain>().dna, parent2.GetComponent<Brain>().dna);
            }
            return offspring;
        }

        private void BreedNewPopulation() {

            List<GameObject> sortedList = population.OrderBy(
                o => o.GetComponent<Brain>().distatanceTravelled).ToList();

            population.Clear();

            for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; ++i) {

                population.Add(Breed(sortedList[i], sortedList[i + 1]));
                population.Add(Breed(sortedList[i + 1], sortedList[i]));
            }

            // Destroy all parents and previous population
            for (int i = 0; i < sortedList.Count; ++i) {

                Destroy(sortedList[i]);
            }
            generation++;
        }

        void Update() {

            elapsed += Time.deltaTime;
            if (elapsed >= trialTime) {

                BreedNewPopulation();
                elapsed = 0.0f;
            }
        }
    }
}