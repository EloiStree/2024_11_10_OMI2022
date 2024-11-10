using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverChangeColor : MonoBehaviour
{
   
    private void OnMouseOver()
    {
        //GetComponent<Renderer>().material.color = new Color(Random.value, 0,0,1);
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = new Color(Random.value, 0,0, 1);

    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = new Color(0, Random.value, 0,1);

    }
}
