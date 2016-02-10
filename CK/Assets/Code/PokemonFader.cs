using UnityEngine;
using System.Collections;

public class PokemonFader : MonoBehaviour {

	public GameObject Black;
	public GameObject White;

	public GameObject UI;

	private AudioSource audiosource;
	private bool visible = false;

	private SpriteRenderer rendererer;
	private bool canFade = true;


	// Use this for initialization
	void Start() {
		rendererer = White.GetComponent<SpriteRenderer>();
		rendererer.color = new Color( 1, 1, 1, 0 );


		audiosource = GetComponent<AudioSource>();

		StartFade();
		StartCoroutine( StopFade() );
	}

	void FadeUpdate( float value ) {
		rendererer.color = new Color( 1, 1, 1, value );
	}

	IEnumerator StopFade() {
		yield return new WaitForSeconds( 2.5f );
		canFade = false;
	}

	void StartFade() {
		if ( canFade ) {
			iTween.ValueTo( gameObject, iTween.Hash( "time", 0.15f,
				"onupdate", "FadeUpdate",
				"oncomplete", "StartFade",
				"from", visible ? 1 : 0, "to", visible ? 0 : 1 ) );
			visible = !visible;
		} else {
			Destroy( Black );
			Destroy( White );

			UI.SetActive( true );
		}
	}
}
