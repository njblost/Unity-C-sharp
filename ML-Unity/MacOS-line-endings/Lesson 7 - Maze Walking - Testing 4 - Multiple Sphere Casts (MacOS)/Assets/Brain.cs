using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeWalking
{

    public class Brain : MonoBehaviour
    {

        public int DNALength = 2;
        public DNA dna;
        public GameObject eyes;
        public float distatanceTravelled = 0.0f;
        private bool canSeeWallFront = false;
        private bool canSeeWallLeft = false;
        private bool canSeeWallRight = false;
        private Vector3 startPosition;
        private bool isAlive = true;

        public void Init()
        {
            // Initialise DNA
            // 0 Forward
            // 1 Angle Turn
            dna = new DNA(DNALength, 360);
            startPosition = this.transform.position;
        }

        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "dead")
            {
                distatanceTravelled = 0.0f;
                isAlive = false;
            }
        }

        private void Update()
        {
            // Reset wall detection flags
            canSeeWallFront = false;
            canSeeWallLeft = false;
            canSeeWallRight = false;

            float detectionRange = 1.5f; // Increase detection range

            // Raycast forward
            Debug.DrawRay(eyes.transform.position, eyes.transform.forward * detectionRange, Color.red);
            if (Physics.SphereCast(eyes.transform.position, 0.1f, eyes.transform.forward, out RaycastHit hitFront, detectionRange))
            {
                if (hitFront.collider.gameObject.tag == "wall") canSeeWallFront = true;
            }

            // Raycast to the right
            Debug.DrawRay(eyes.transform.position, eyes.transform.right * detectionRange, Color.green);
            if (Physics.SphereCast(eyes.transform.position, 0.1f, eyes.transform.right, out RaycastHit hitRight, detectionRange))
            {
                if (hitRight.collider.gameObject.tag == "wall") canSeeWallRight = true;
            }

            // Raycast to the left
            Debug.DrawRay(eyes.transform.position, -eyes.transform.right * detectionRange, Color.blue);
            if (Physics.SphereCast(eyes.transform.position, 0.1f, -eyes.transform.right, out RaycastHit hitLeft, detectionRange))
            {
                if (hitLeft.collider.gameObject.tag == "wall") canSeeWallLeft = true;
            }
        }

        private void FixedUpdate()
        {
            if (!isAlive) return;

            // Read DNA
            float h = 0.0f;
            float v = dna.GetGene(0);  // Forward movement

            // Adjust turning based on wall detection
            if (canSeeWallFront)
            {
                // If front is blocked, turn based on side detection
                if (canSeeWallLeft && !canSeeWallRight)
                {
                    h = dna.GetGene(1);  // Turn right if left is blocked
                }
                else if (canSeeWallRight && !canSeeWallLeft)
                {
                    h = -dna.GetGene(1); // Turn left if right is blocked
                }
                else if (canSeeWallLeft && canSeeWallRight)
                {
                    h = dna.GetGene(1);  // Turn around if both sides are blocked
                }
            }
            else if (canSeeWallLeft && !canSeeWallRight)
            {
                h = dna.GetGene(1);  // Turn right if only left is blocked
            }
            else if (canSeeWallRight && !canSeeWallLeft)
            {
                h = -dna.GetGene(1); // Turn left if only right is blocked
            }

            this.transform.Translate(0.0f, 0.0f, v * 0.0007f);
            this.transform.Rotate(0.0f, h, 0.0f);
            distatanceTravelled = Vector3.Distance(startPosition, this.transform.position);
        }
    }
}
