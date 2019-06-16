using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{

    public GameObject tree;

    // Start is called before the first frame update
    void Start()
    {
        int numTrees = Random.Range(50, 75);

        for(int i = 0; i < numTrees; i++)
        {
            Instantiate(tree, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), Quaternion.identity);
        }
    }
}
