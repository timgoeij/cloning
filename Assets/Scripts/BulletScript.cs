using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	private float _verticalDirection = 0;
	private float _horizontalDirection = 0;
	private float _speed = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	public void SetDirection(float vertical, float horizontal) {
		_verticalDirection = vertical;
		_horizontalDirection = horizontal;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (_verticalDirection != 0) {
			transform.Translate( Vector3.forward * _verticalDirection * _speed );
		}

		if (_horizontalDirection != 0) {
			transform.Translate( Vector3.right * _horizontalDirection * _speed );
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<EnemyScript>() != null) {
			other.GetComponent<EnemyScript>().Hit();
			Destroy(gameObject);
		}
	}
}
