using UnityEngine;
using System.Collections;

public class Flincher : MonoBehaviour {

	private bool spliffEnabled = false;
	private int times = 10;
	private int amountAttacks = 5;
	private bool firstTime = true;

	public GameObject SpliffThing;
	public GameObject UI;

	public GameObject explosion;

	public void Flinch( float seconds ) {
		

		if ( amountAttacks <= 0 && firstTime ) {
			amountAttacks = 5;
			firstTime = false;
			StartCoroutine( StartUI( seconds ) );
		} else if ( amountAttacks <= 0 && !firstTime ) {
			//Destroy( gameObject );
			GetComponent<Renderer>().enabled = false;
			explosion.SetActive( true );
			StartCoroutine( StopNuke() );
		} else {
			StartCoroutine( DoFlinch( seconds ) );
		}

		amountAttacks--;
	}

	IEnumerator StopNuke() {
		yield return new WaitForSeconds( 2.1f );
		Destroy( explosion );
		Application.LoadLevel( "CreditsScene" );
	}

	IEnumerator StartUI( float seconds ) {
		yield return new WaitForSeconds( seconds );
		SpliffThing.SetActive( true );
		UI.SetActive( false );
	}

	IEnumerator DoFlinch( float seconds ) {
		times = 10;
		yield return new WaitForSeconds( seconds );
		StartCoroutine( Blink() );
	}

	IEnumerator Blink() {
		var r = GetComponent<Renderer>();

		r.enabled = false;
		yield return new WaitForSeconds( 0.015f );
		r.enabled = true;

		if ( times > 0 ) {
			times--;
			yield return new WaitForSeconds( 0.015f );
			StartCoroutine( Blink() );
		}
	}
}