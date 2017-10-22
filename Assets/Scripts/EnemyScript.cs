using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	private int _health = 2;

	public void Hit() {
		_health--;

		if (_health <= 0) {
			Destroy(gameObject);
		}
	}
}
