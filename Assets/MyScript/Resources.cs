using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Resources : MonoBehaviour {
    public int wood = 0;
    public Text woodText;
    public int stone = 0;
    public Text stoneText;
    public GameObject resources;
	// Use this for initialization
	void Start () {
        resources = this.gameObject;
        woodText = resources.transform.Find("Wood").gameObject.GetComponent<Text>();
        stoneText = resources.transform.Find("Stone").gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        woodText.text = Convert.ToString(wood);
        stoneText.text = Convert.ToString(stone);
    }
}
