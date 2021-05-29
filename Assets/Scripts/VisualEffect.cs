using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script attaches to ‘VisualEffect’ objects. It destroys or deactivates them after the defined time.
/// </summary>
public class VisualEffect : MonoBehaviour {

    // time to destroy VFX object
    public float destructionTime;

    private void OnEnable()
    {
        StartCoroutine(Destruction()); 
    }

    // wait for estimated time to destroy the object
    IEnumerator Destruction() 
    {
        yield return new WaitForSeconds(destructionTime); 
        Destroy(gameObject);
    }
}
