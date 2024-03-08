using UnityEngine;

public class WorldScript : MonoBehaviour
{
    public new BoxCollider collider;


    public GameObject[] trees;
    public Transform treeParent;

    public GameObject ruins;
    public Transform ruinParent;

    public GameObject[] rocks;
    public Transform rockParent;

    void Start()
    {
        spawnTrees();
        spawnRuins();
        spawnRocks();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnTrees()
    {
        int width = (int)collider.bounds.size.x;
        int height = (int)collider.bounds.size.z;
        for (float x = -1 * (width / 2); x < width / 2; x++)
        {
            for (float y = -1 * (height / 2); y < height / 2; y++)
            {
                float sample = Mathf.PerlinNoise(width + x * 1.1f, height + y * 1.1f);

                //tree spawn
                if (sample > 0.99)
                {
                    int randRange = Random.Range(0, trees.Length);
                    Instantiate(trees[randRange], new Vector3(x, 0, y), Quaternion.Euler(0, Random.Range(0, 360), 0), treeParent);
                }
            }
        }
    }

    public void spawnRuins()
    {
        int width = (int)collider.bounds.size.x;
        int height = (int)collider.bounds.size.z;
        for (int i = 0; i < 20; i++)
        {
            int randNum1 = Random.Range(-(width / 2 - 5), width / 2 - 5);
            int randNum2 = Random.Range(-(height / 2 - 5), height / 2 - 5);
            float randRotation = Random.Range(0, 360);
            while (!(randRotation >= 45 && randRotation <= 135) && !(randRotation >= 225 && randRotation <= 315))
            {
                randRotation = Random.Range(0, 360);
            }
            Instantiate(ruins, new Vector3(randNum1, 0.6f, randNum2), Quaternion.Euler(0, randRotation, 0),ruinParent);

        }
    }

    public void spawnRocks()
    {
        int width = (int)collider.bounds.size.x;
        int height = (int)collider.bounds.size.z;
        for (int i = 0; i < 50; i++)
        {
            int randNum1 = Random.Range(-(width / 2 - 5), width / 2 - 5);
            int randNum2 = Random.Range(-(height / 2 - 5), height / 2 - 5);
            float randRotation = Random.Range(0, 360);
            int randRange = Random.Range(0, rocks.Length);
            GameObject nerd = Instantiate(rocks[randRange], new Vector3(randNum1, 0, randNum2), Quaternion.Euler(0, Random.Range(0, 360), 0), rockParent);
            float randScale = Random.Range(0.5f, 2);
            nerd.transform.localScale = new Vector3(randScale, randScale, randScale);
        }
    }
}
