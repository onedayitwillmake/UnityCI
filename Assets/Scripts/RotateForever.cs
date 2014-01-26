using UnityEngine;
using System.Collections;

public class RotateForever : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate((Vector3.right + Vector3.up) * Time.deltaTime * 50);
	}
}
