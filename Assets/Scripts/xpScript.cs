using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering;

public class XPScript : MonoBehaviour
{
    public int expLvl;
    public GameObject expHigher;
    Transform player_position;

    void Start()
    {
        player_position = GameObject.Find("Player").transform;
    }

    void Awake(){
        gameObject.GetComponent<CondenseGems>().collisionatorationalizationarilly();
    }

    void FixedUpdate(){
        if(Vector3.Distance(player_position.transform.position, transform.position) < 5 * (UpgradeUi.getUpgradeInfo(UpgradeList.magnetUp).value +1)){
            XPSystem.changeExp(expLvl);
            Destroy(transform.gameObject);
            
        }
    }
    
}
