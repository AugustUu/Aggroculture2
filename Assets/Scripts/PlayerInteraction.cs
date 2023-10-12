using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public new Camera camera;
    public GameObject placeable;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 300f, 1 << 6))
            {
                GameObject farmland = Instantiate(placeable, new Vector3(hit.point.x - (hit.point.x % 2.5f), hit.point.y, hit.point.z - (hit.point.z % 2.5f)), Quaternion.identity);
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 1f);
                Debug.Log(hit.point);
            }
        }
    }
}
