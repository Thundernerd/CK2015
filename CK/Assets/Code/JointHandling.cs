using UnityEngine;
using System.Collections;

public class JointHandling : MonoBehaviour {

	private AudioSource audiosource;

	public GameObject[] childrensesns;

	public GameObject HandLeft;
	public GameObject HandRight;

	public GameObject OtherSky;

	public GameObject Nuke1;
	public GameObject Nuke2;

	public AudioClip RammClip;

	public GameObject uibase;
	public GameObject uispec;
	public GameObject uiother;

	public GameObject pokemonstuff;

	private bool inverted = false;
	private bool allow = false;
	private float moveTime = 1f;
	private float scaleTime = 1f;

	// Use this for initialization
	void Start() {
		audiosource = GetComponent<AudioSource>();

		foreach ( var item in childrensesns ) {
			item.SetActive( false );
		}
	}

	void Update() {
		if ( !allow ) return;

		if ( Input.touchCount > 0 || Input.GetMouseButtonDown( 0 ) ) {
			allow = false;

			StartSpecial();
		}
	}

	public void DoTheJoint() {
		foreach ( var item in childrensesns ) {
			item.SetActive( true );
		}

		audiosource.Play();
		allow = true;
		Destroy( pokemonstuff );
	}

	private IEnumerator FireNuke( float seconds, GameObject nuke ) {
		yield return new WaitForSeconds( seconds );
		nuke.SetActive( true );
		StartCoroutine( KillNuke( nuke ) );
	}

	private IEnumerator KillNuke( GameObject nuke ) {
		yield return new WaitForSeconds( 2.1f );
		Destroy( nuke );
	}

	private void StopTheJoint() {
		iTween.Stop( gameObject );
		audiosource.Stop();
		audiosource.clip = RammClip;
		audiosource.Play();

		StartCoroutine( FireNuke( 40.5f, Nuke1 ) );
		StartCoroutine( FireNuke( 40.7f, Nuke2 ) );

		foreach ( var item in childrensesns ) {
			item.SetActive( false );
		}

		OtherSky.SetActive( true );

		uibase.SetActive( true );
		uispec.GetComponent<UnityEngine.UI.Button>().interactable = true;

		uiother.SetActive( true );

		// #Don'tStopTheJoint
	}

	public void StartSpecial() {
		MoveComplete();
		ScaleComplete();
	}

	void MoveComplete() {
		if ( moveTime <= 0.05f ) {
			StopTheJoint();
		} else {
			moveTime *= 0.9f;
			iTween.ValueTo( gameObject, iTween.Hash( "from", inverted ? 5 : -5, "to", inverted ? -5 : 5, "time", moveTime, "onupdate", "MoveUpdate", "oncomplete", "MoveComplete" ) );
		}
	}

	void ScaleComplete() {
		if ( scaleTime <= 0.05f ) {
			StopTheJoint();
		} else {
			scaleTime *= 0.9f;
			iTween.ValueTo( gameObject, iTween.Hash( "from", inverted ? 0.5f : -0.5f, "to", inverted ? -0.5f : 0.5f, "time", scaleTime, "onupdate", "ScaleUpdate", "oncomplete", "ScaleComplete" ) );
		}
	}

	void MoveUpdate( float value ) {
		var position = new Vector3( 0, 0, 0 );

		HandLeft.transform.position = position + new Vector3( value, 0 );
		HandRight.transform.position = position - new Vector3( value, 0 );
	}

	void ScaleUpdate( float value ) {
		var scale = new Vector3( 0.75f, 0.75f, 0.75f );

		HandLeft.transform.localScale = scale + new Vector3( value, value );
		HandRight.transform.localScale = scale - new Vector3( value, value );
	}
}
