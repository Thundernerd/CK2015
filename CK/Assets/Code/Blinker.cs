using UnityEngine;
using System.Collections;

public class Blinker : MonoBehaviour {

	new private Renderer renderer;

	// Use this for initialization
	void Start() {
		renderer = GetComponent<MeshRenderer>();

		StartCoroutine( Blink() );
	}

	private IEnumerator Blink() {
		yield return new WaitForSeconds( 1 );
		renderer.enabled = !renderer.enabled;
		StartCoroutine( Blink() );
	}
}
