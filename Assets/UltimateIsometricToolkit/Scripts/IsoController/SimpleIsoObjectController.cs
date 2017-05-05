using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;
using System;

namespace UltimateIsometricToolkit.controller { 
/// <summary>
/// Simple continuous movement with WSAD/Arrow Keys movement.
/// Note: This is an exemplary implementation. You may vary inputs, speeds, etc.
/// </summary>

	public class SimpleIsoObjectController : MonoBehaviour {

		public float Speed = 10;
		
		private IsoTransform _isoTransform;
        private Vector3 initialMousePosition;
        Vector3 mouseposition;

        void Awake()
        {
			//_isoTransform = this.GetOrAddComponent<IsoTransform>(); //avoids polling the IsoTransform component per frame
		}

        void Update()
        {
           
           

            //translate on isotransform
            if (Input.GetMouseButton(0))
            {
                mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Input.GetMouseButtonDown(0))
                {
                    initialMousePosition = mouseposition;
                }


                if (initialMousePosition != mouseposition)
                {
                    if (Math.Abs(mouseposition.x - initialMousePosition.x) > 2 && Math.Abs(mouseposition.y - initialMousePosition.y) > 2)
                    {
                        transform.Translate(new Vector3(-(mouseposition.x - initialMousePosition.x), -(mouseposition.y - initialMousePosition.y), 0) * Time.deltaTime * Speed);
                    }
                    else
                    {
                        if (Math.Abs(mouseposition.x - initialMousePosition.x) > 2)
                        {
                            transform.Translate(new Vector3(-(mouseposition.x - initialMousePosition.x), 0, 0) * Time.deltaTime * Speed);
                        }
                        if(Math.Abs(mouseposition.y - initialMousePosition.y) > 2)
                        {
                            transform.Translate(new Vector3(0, -(mouseposition.y - initialMousePosition.y), 0) * Time.deltaTime * Speed);
                        }
                    }
                }
                
            }
        }
         
	}
}
