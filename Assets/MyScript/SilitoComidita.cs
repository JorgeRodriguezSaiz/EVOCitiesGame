using UnityEngine;
using System.Collections;

public class SilitoComidita : MonoBehaviour {
    public GameObject siloWindow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        siloWindow.SetActive(true);
    }
}
