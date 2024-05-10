using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MobDamageTrigger : MonoBehaviour
{
    [SerializeField]
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.GetComponent<NavMeshAgent>().remainingDistance);
        if (gameObject.GetComponent<NavMeshAgent>().remainingDistance <= 3){
            animator.SetBool("hitPlayer", true);
        }
        else{
            animator.SetBool("hitPlayer", false);
        }
    }
}
