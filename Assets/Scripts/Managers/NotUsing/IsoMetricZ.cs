using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IsoMetricZ : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 100);
    }
}
