using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    public bool isMovingToPlayer = false;
    public int worth;

    private float speed = 20f;
    private GameObject target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isMovingToPlayer)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

            if (Vector3.Distance(transform.position, target.transform.position) < 0.001f)
            {
                //target.GetComponent<PlayerStats>().money += worth;
                Destroy(gameObject);
            }
        }*/
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        //target.GetComponent<PlayerStats>().money += worth;
    }
}
