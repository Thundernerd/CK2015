using UnityEngine;
using System.Collections;

public class CodeLauncher : MonoBehaviour {

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public void FireCode() {
		var t = GetComponent<Renderer>().material.mainTexture as MovieTexture;
		t.Play();
		StartCoroutine( DisablePlane( t.duration ) );
	}

	IEnumerator DisablePlane( float seconds ) {
		yield return new WaitForSeconds( seconds );
		gameObject.SetActive( false );
	}
}
