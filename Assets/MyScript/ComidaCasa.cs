using UnityEngine;
using System.Collections;
using Assets.UltimateIsometricToolkit.Scripts.Core;

public class ComidaCasa : MonoBehaviour {
    public float comidaCasa;
    public float comidaCasaTotal;
    public bool isComida = true;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(comidaCasa <= 0)
        {
            isComida = false;
        }
        if (gameObject.GetComponent<Casa>().enabled == false && comidaCasa > 0)
        {
            comidaCasa -= (float)0.1 * Time.deltaTime;
        }
	}
    public void OnMouseDown()
    {
        if (comidaCasaTotal - comidaCasa > 1)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().comida -= (int)(comidaCasaTotal - comidaCasa);
            comidaCasa = comidaCasaTotal;
            isComida = true;
        }
    }
}
