using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTerrain : MonoBehaviour
{
    public new BoxCollider collider;
    public GameObject[] terrain;

    public float test;
    // Start is called before the first frame update
    void Start()
    {
        int width = (int)collider.bounds.size.x;
        int height = (int)collider.bounds.size.z;
        for (int i = 0; i < test; i++)
        {
            int randNum1 = Random.Range(-(width / 2 - 5), width / 2 - 5);
            int randNum2 = Random.Range(-(height / 2 - 5), height / 2 - 5);
            Instantiate(terrain[Random.Range(0, terrain.Length)], new Vector3(randNum1, 0.6f, randNum2), Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
