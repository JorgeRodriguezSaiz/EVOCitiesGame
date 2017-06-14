using UnityEngine;
using System.Collections;

public class CombustionEspontaneaArborea : MonoBehaviour {
    public bool madera = true; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnMouseDown()
    {
        if (gameObject.tag == "arbol")
        {
            madera = true;
        }
        if (gameObject.tag == "piedra")
        {
            madera = false;
        }
        GameObject.Find("Controller").GetComponent<ControllerTalar>().panelTalar.SetActive(true);
        GameObject.Find("Controller").GetComponent<ControllerTalar>().arbol = gameObject;
    }
}
