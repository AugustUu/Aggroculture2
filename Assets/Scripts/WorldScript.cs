using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScript : MonoBehaviour
{
    public new BoxCollider collider;

    public GameObject tree;
    void Start()
    {
        int width = (int)collider.bounds.size.x;
        int height = (int)collider.bounds.size.z;
        for (float x = -1 * (width / 2); x < width / 2; x++)
        {
            for (float y = -1 * (height / 2); y < height / 2; y++)
            {
                //Debug.Log(x);
                
                float sample = Mathf.PerlinNoise(x * 1.1f, y * 1.1f);

                if(sample > 0.92){
                    //Debug.Log(x + " " + y);
                    Instantiate(tree, new Vector3(x,0,y),Quaternion.identity);
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
