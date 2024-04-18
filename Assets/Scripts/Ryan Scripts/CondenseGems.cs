using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class CondenseGems : MonoBehaviour
{
    public LayerMask layerMask;
    public static Vector3 condenseSize = new Vector3(10,10,10);
    private Transform xpParent;
    // Start is called before the first frame update
    void Start()
    {
        xpParent = GameObject.Find("EXP").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos(){
        Gizmos.color = Color.cyan;
        //Gizmos.DrawWireCube(gameObject.transform.position, condenseSize * 2);
    }

    public void collisionatorationalizationarilly(){
        Collider[] gems = Physics.OverlapBox(gameObject.transform.position, condenseSize, Quaternion.identity, layerMask);
        if (gems.Length - 1 >= 4){
            if (gameObject.GetComponent<XPScript>().expHigher != null){
                Instantiate(gameObject.GetComponent<XPScript>().expHigher, this.transform.position, this.transform.rotation,xpParent);
                for (int i =0;i<gems.Length;i++){
                    Destroy(gems[i].gameObject);
                }
            }
        }
    }
}
