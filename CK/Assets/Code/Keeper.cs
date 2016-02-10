using UnityEngine;
using System.Collections;

public class Keeper : MonoBehaviour {
	
	void Awake() {
		DontDestroyOnLoad( transform.gameObject );
	}
}
