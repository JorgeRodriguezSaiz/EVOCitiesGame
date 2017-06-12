using UnityEngine;
using System.Collections;

public class CombustionEspontaneaArborea : MonoBehaviour {
    public GameObject panelSeguro;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnMouseDown()
    {
        panelSeguro.SetActive(true);
    }
}
