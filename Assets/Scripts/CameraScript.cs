using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	private Transform _targetPosition;

	// Use this for initialization
	void Start () {
		_targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = _targetPosition.position;
		newPos.y += 7.5f;
		newPos.x += 7.5f;

		transform.position = Vector3.Lerp(transform.position, newPos, 1f);
	}
}
