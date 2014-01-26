using UnityEngine;
using System.Collections;

public class DevProdLabel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
#if CI_MODE_DEV
		GUI.Label(new Rect(10,10,100,100), "Mode: CI_MODE_DEV");
#elif CI_MODE_PROD
		GUI.Label(new Rect(10,10,100,100), "Mode: CI_MODE_PROD");
#else
		GUI.Label(new Rect(10,10,100,100), "Mode: Unknown");
#endif
	}
}
