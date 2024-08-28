using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeWalking {

    public class Brain : MonoBehaviour {

        public int DNALength = 2;
        public DNA dna;
        public GameObject eyes;
        public float distatanceTravelled = 0.0f;
        private bool canSeeWall = true;
        private Vector3 startPosition;
        private bool isAlive = true;

        public void Init() {

            // Initialise DNA
            // 0 Forward
            // 1 Angle Turn
            dna = new DNA(DNALength, 360);
            startPosition = this.transform.position;
        }

        private void OnCollisionEnter(Collision col) {


            if (col.gameObject.tag == "dead") {

                distatanceTravelled = 0.0f;
                isAlive = false;
            }
        }

        private void Update() {

            canSeeWall = false;
            Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 0.5f, Color.red);
            if (Physics.SphereCast(eyes.transform.position, 0.1f, eyes.transform.forward, out RaycastHit hit, 0.5f)) {

                if (hit.collider.gameObject.tag == "wall") canSeeWall = true;
            }
        }

        private void FixedUpdate() {

            if (!isAlive) return;

            // Read DNA
            float h = 0.0f;
            float v = dna.GetGene(0);

            if (canSeeWall) h = dna.GetGene(1);

            this.transform.Translate(0.0f, 0.0f, v * 0.0001f);
            this.transform.Rotate(0.0f, h, 0.0f);
            distatanceTravelled = Vector3.Distance(startPosition, this.transform.position);
        }
    }
}