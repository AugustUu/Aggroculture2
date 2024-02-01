using UnityEngine;

public class WorldScript : MonoBehaviour
{
    public new BoxCollider collider;


    public GameObject[] trees;
    void Start()
    {

        int width = (int)collider.bounds.size.x;
        int height = (int)collider.bounds.size.z;
        for (float x = -1 * (width / 2); x < width / 2; x++)
        {
            for (float y = -1 * (height / 2); y < height / 2; y++)
            {
                //Debug.Log(x);

                float sample = Mathf.PerlinNoise(width + x * 1.1f, height + y * 1.1f);

                if (sample > 0.99)
                {
                    // Instantiate(trees[Random.Range(0, trees.Length)], new Vector3(x, 8, y), Quaternion.Euler(0,Random.Range(0, 180),0));
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
