using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeAlg2 : MonoBehaviour
{

    public GameObject blockPrefab;
    int width = 41;
    int depth = 41;

    // Use this for initialization
    void Awake()
    {
        for (int w = 0; w < width; w++)
        {
            for (int d = 0; d < depth; d++)
            {
                if (w == 0 || d == 0)   //outside walls bottom and left
                {
                    Instantiate(blockPrefab, new Vector3(w + this.transform.position.x, this.transform.position.y, d + this.transform.position.z), Quaternion.identity);
                }
                else if (w < 6 && d < 6)
                {
                    continue;
                }
                else if (w == width - 2 && d == depth - 1)
                {
                    continue;
                }
                else if (w == width - 1 || d == depth - 1) //outside walls top and right
                {
                    Instantiate(blockPrefab, new Vector3(w + this.transform.position.x, this.transform.position.y, d + this.transform.position.z), Quaternion.identity);
                }
                else if (w > width - 7 && d > depth - 7)
                {
                    continue;
                }
                else if ((w % 2 != 0) && (d % 2 != 0))
                {
                    continue;
                }
                else if ((w % 2 == 0) && (d % 2 == 0))
                {
                    Instantiate(blockPrefab, new Vector3(w + this.transform.position.x, this.transform.position.y, d + this.transform.position.z), Quaternion.identity);
                }
                else if (Random.Range(0, 3) < 1)
                {
                    Instantiate(blockPrefab, new Vector3(w + this.transform.position.x, this.transform.position.y, d + this.transform.position.z), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}