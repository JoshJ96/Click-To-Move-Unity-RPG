using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedLightFadeOut : MonoBehaviour
{
    //This is the marker that pops up when you click an area to walk to.

    void OnEnable()
    {
        
    }

    void Update()
    {
        GetComponent<Light>().intensity -= 3 * Time.deltaTime;
        GetComponent<Light>().range -= 3 * Time.deltaTime;

        if (GetComponent<Light>().intensity == 0 && GetComponent<Light>().range == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
