using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CQCcontroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("kick");
        if (other.CompareTag(KnownTags.Enemy))
        {
            Debug.Log("kick");
        }
    }
}
