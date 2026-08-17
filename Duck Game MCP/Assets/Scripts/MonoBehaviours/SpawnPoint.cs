using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Optional Prefab Spawning")]
    [SerializeField] private GameObject objectPrefab;
    
    [Header("Spawn Settings")]
    [SerializeField] private bool useSpawnPointRotation = true;
    
    public Vector3 SpawnPosition => transform.position;

    public Quaternion SpawnRotation
    {
        get
        {
            if (useSpawnPointRotation)
            {
                return transform.rotation;
            }
            
            return Quaternion.identity;
        }
    }

    public GameObject SpawnObject()
    {
        if (objectPrefab == null)
        {
            Debug.LogWarning(gameObject.name + "cannot spawn because no Object Prefab is assigned.");
            return null;
        }
        // CONTINUE AUGUST 22
    }
}
