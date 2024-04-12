using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class CondenseGems : MonoBehaviour
{
    public LayerMask layerMask;
    private Vector3 condenseSize;
    // Start is called before the first frame update
    void Start()
    {
        condenseSize = new Vector3(100,100,100);
    }

    // Update is called once per frame
    void Update()
    {
        ihatelayermasksihatelayermasks
        Collider[] hehe = Physics.OverlapBox(gameObject.transform.position, condenseSize, Quaternion.identity, layerMask);
        if (hehe.Length - 1 >= 5){
            Debug.Log("haha");
        }
       
    }

    void OnDrawGizmos(){
        //Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(gameObject.transform.position, condenseSize * 2);
    }

    void collisionatorationalizationarilly(){

    }
}
