using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class envirmentSanity : MonoBehaviour
{
    // get all the light in the scene and store them in an array
    public Light[] lights;

    public Light mainLight;


    public HealthBar sanityBar;

    //public float brightness = 1.0f;
    //public Color color = Color.white;
    //public float saturation = 1.0f;
    void Start()
    {
        // get all the light in the scene and store them in an array
        lights = FindObjectsOfType<Light>();

    }

    // Update is called once per frame
    void Update()
    {

        if (sanityBar.GetHealth() > 50)
        {
            //loop over lights array
            foreach (Light light in lights)
            {
                light.color = Color.white;
                light.intensity = 0.0f;
            }

            mainLight.intensity = 1.0f;

        }


        if (sanityBar.GetHealth() < 50)
        {
            //loop over lights array
            foreach (Light light in lights)
            {
                light.color = Color.red;
                light.intensity = 1.0f;
            }

        }



    }
}
