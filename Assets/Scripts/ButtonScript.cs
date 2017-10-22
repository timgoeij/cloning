using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

	[SerializeField]
	private GameObject _door;

	private Collider _other;
	private bool _triggered = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_triggered && !_other) {
			_door.SetActive(true);
		}
	}

	void OnCollisionEnter(Collision collision) {
		_door.SetActive(false);
		
		if (collision.collider.tag != "Player") {
			_other = collision.collider;
			_triggered = true;
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.collider.tag == "Player" && (! _triggered || _other)) {
			_door.SetActive(true);
		}
	}
}
