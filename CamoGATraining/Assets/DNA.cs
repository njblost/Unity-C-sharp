using System.Collections;=
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    // Gene for colour
    public float r;
    public float g;
    public float b;=

    // Store whether a player has been clicked on (i.e. they have died)
    bool dead = false;

    public float timeToDie = 0;

    SpriteRenderer sRenderer;
    Collider2D sCollider;

    // On mouse code for clicking on colliders - when the user clicks on the person, "dead" becomes true and timeToDo will be set to time elapsed in the population manager

    void OnMouseDown()
    {
        dead = true;
        timeToDie = PopulationManager.elsapsed;
        Debug.Log("Dead At: " + timeToDie);

        // When the person is considered "dead" the collider and renderer are disabled
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
