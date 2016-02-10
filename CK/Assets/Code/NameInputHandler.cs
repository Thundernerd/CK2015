using UnityEngine;
using System.Collections;

public class NameInputHandler : MonoBehaviour {

	public AudioClip[] Clips;

	private char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

	new private string name = "_________";
	private bool touchBegan = false;
	private int index = 0;
	private bool allow = true;
	private bool doubleAllow = true;

	private TextMesh textmesh;
	private AudioSource audiosource;


	// Use this for initialization
	void Start() {
		textmesh = GetComponent<TextMesh>();
		audiosource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update() {
		bool touched = Touchdown() || Input.GetMouseButtonDown( 0 );

		if ( !allow ) {
			if ( !doubleAllow ) return;

			iTween.CameraFadeAdd( iTween.CameraTexture( Color.black ), 0 );
			iTween.CameraFadeTo( 1, 3f );
			StartCoroutine( LoadNextLevel() );

			doubleAllow = false;
		} else {
			if ( touched ) {
				var letter = alphabet[Random.Range( 0, 25 )];
				name = name.Remove( index, 1 );
				name = name.Insert( index, letter.ToString() );
				index++;

				if ( index == 9 ) {
					allow = false;
					name = "dotKokott";
				}

				textmesh.text = name;

				audiosource.PlayOneShot( Clips[Random.Range( 0, 4 )] );
			}
		}
	}

	private bool Touchdown() {
		if ( Input.touchCount > 0 ) {
			var touch = Input.GetTouch( 0 );

			if ( touch.phase == TouchPhase.Began ) {
				touchBegan = true;
				return false;
			}

			if ( touch.phase == TouchPhase.Ended && touchBegan ) {
				touchBegan = false;
				return true;
			}
		}

		return false;
	}

	private IEnumerator LoadNextLevel() {
		yield return new WaitForSeconds( 3f );
		Destroy( GameObject.Find( "Elevator Music" ) );
		Application.LoadLevel( "FightScene" );
	}
}
