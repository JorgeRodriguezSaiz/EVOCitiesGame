using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace Assets.UltimateIsometricToolkit.Scripts.Core
{
    public class VolumenFx : MonoBehaviour
    {
        GameObject[] allAudioSources;
        public GameObject size, imagen;
        // Use this for initialization
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {
            imagen.GetComponent<RectTransform>().anchorMax = new Vector2(size.GetComponent<Scrollbar>().value, 1);
            CambiarVolumenFx();
        }
        public void CambiarVolumenFx()
        {
            allAudioSources = GameObject.FindGameObjectsWithTag("FX");
            foreach (GameObject audioS in allAudioSources)
            {
                audioS.GetComponent<AudioSource>().volume = size.GetComponent<Scrollbar>().value;
            }
        }
    }
}
