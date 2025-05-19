using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Payer"))
        {
            BananaCollector.Instance.Collect();
            Destroy(gameObject);
        }
    }

}
