using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;

public class CondenseGems : MonoBehaviour
{
    public LayerMask layerMask;
    public static Vector3 condenseSize = new Vector3(10,10,10);
    public static Transform xpParent;
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
        if (gems.Length >= 5){
            if (gameObject.GetComponent<XPScript>().expHigher != null){
                Instantiate(gameObject.GetComponent<XPScript>().expHigher, new Vector3(
                    gems.Take(5).Average(x=>x.transform.position.x),
                    gems.Take(5).Average(x=>x.transform.position.y),
                    gems.Take(5).Average(x=>x.transform.position.z)
                ), this.transform.rotation,xpParent);
                for (int i = 0; i < 5; i++){
                    Destroy(gems[i].gameObject);
                }
            }
        }
    }
}
