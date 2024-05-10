using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinVisbility : MonoBehaviour
{
    public GameObject[] Wall1;
    public GameObject[] Wall2;
    public GameObject[] Wall3;
    public GameObject[] Wall4;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        for (int x = 0; x < 2; x++)
        {
            Wall2[x].SetActive(false);
            Wall3[x].SetActive(false);
            Wall4[x].SetActive(false);
        }
        Wall2[Random.Range(0, 2)].SetActive(true);
        Wall3[Random.Range(0, 2)].SetActive(true);
        Wall4[Random.Range(0, 2)].SetActive(true);
        if (Random.Range(0,11) > 9){
            transform.Find("chest").gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
