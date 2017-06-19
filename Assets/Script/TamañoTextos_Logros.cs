using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TamañoTextos_Logros : MonoBehaviour {
    private float startW;
    private int numero;
    // Use this for initialization
    void Start () {
        startW = Screen.width;
        numero = Mathf.FloorToInt(startW * ((25f - 16f) / (1232f - 741f)));
        gameObject.GetComponent<Text>().fontSize = numero;
    }
	
	// Update is called once per frame
	void Update () {
        if (startW != Screen.width)
        {
            startW = Screen.width;
            numero = Mathf.FloorToInt(startW * ((25f - 16f) / (1232f - 741f)));
            gameObject.GetComponent<Text>().fontSize = numero;
        }
    }
}
