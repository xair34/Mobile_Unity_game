using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DirectMoving : MonoBehaviour {

    // spee of which to move the object on Y-axis
    public float speed;
    
    // move object on Y-axis
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); 
    }
}
