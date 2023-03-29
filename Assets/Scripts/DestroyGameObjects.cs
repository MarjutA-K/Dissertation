using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjects : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();

    public void AddPlant(GameObject obj)
    {
        objects.Add(obj);
        Debug.Log("Plant List Count: " + objects.Count);
        foreach (var plants in objects)
        {
            Debug.Log("Plant: " + plants.name);
        }
    }

    public void RemovePlant(GameObject obj)
    {
        foreach(var plants in objects)
        {
            objects.Remove(obj);
        }
    }
}
