using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float destroyTime = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        DestroyObject(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
