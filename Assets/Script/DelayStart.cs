using UnityEngine;
using System.Collections;

public class DelayStart : MonoBehaviour {
    public float time = 2;
    public GameObject[] objIn, objOut;
    // Use this for initialization
    void Start () {
        StartCoroutine(Wait());
	}
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(time);
        for (int i = 0; i < objOut.Length; i++)
        {
            objOut[i].SetActive(false);
        }
        for (int i = 0; i < objIn.Length; i++)
        {
            objIn[i].SetActive(true);
        }
    }
}
