using UnityEngine;
using System.Collections;

public class CombustionEspontaneaArborea : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnMouseDown()
    {
        GameObject.Find("Controller").GetComponent<ControllerTalar>().panelTalar.SetActive(true);
        GameObject.Find("Controller").GetComponent<ControllerTalar>().arbol = gameObject;
    }
}
