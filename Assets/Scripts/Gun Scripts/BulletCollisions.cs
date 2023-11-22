using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OntriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {

            Destroy(gameObject);
        }
    }
}
