using UnityEngine;
using System.Collections;

public class TapDetector : MonoBehaviour {

	public AudioSource audioSource;

	private bool allow = true;

	// Update is called once per frame
	void Update() {
		if ( !allow ) return;

		if ( Input.touchCount > 0 || Input.GetMouseButtonDown( 0 ) ) {
			allow = false;
			audioSource.Play();

			iTween.CameraFadeAdd( iTween.CameraTexture( Color.black ), 0 );
			iTween.CameraFadeTo( 1, 2.5f );
			StartCoroutine( LoadNewLevel() );
		}
	}

	private IEnumerator LoadNewLevel() {
		yield return new WaitForSeconds( 2.5f );
		Application.LoadLevel( "NameScene" );
	}
}
