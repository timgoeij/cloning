using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneScript : MonoBehaviour {

	private List<Vector3> _movementPositions;
	private List<RecordedBullet> _recordedShots;  
	private int _recordingIndex = 0;

	// Use this for initialization
	void Start () {

	}
	
	public void SetMovement(List<Vector3> movementPositions) {
		Debug.Log(movementPositions.Count);
		_movementPositions = movementPositions;
	} 

public void SetBullets(List<RecordedBullet> recordedBullets) {
		Debug.Log(recordedBullets.Count);
		_recordedShots = recordedBullets;
	} 

	// Update is called once per frame
	void FixedUpdate () {
		if (_recordingIndex >= _movementPositions.Count) {
			Destroy(gameObject);
			return;
		}

		if (_recordingIndex == 0) {
			transform.position = _movementPositions[ _recordingIndex ];
		} else {
			transform.position = Vector3.MoveTowards(transform.position, _movementPositions[ _recordingIndex ], 1f);
		}

		if ( _recordedShots[ _recordingIndex ].shot != false) {
			GameObject bullet = Instantiate(_recordedShots[ _recordingIndex ].bullet);
			bullet.transform.position = transform.position;
			bullet.GetComponent<BulletScript>().SetDirection(_recordedShots[ _recordingIndex ].verticalDirection, _recordedShots[ _recordingIndex ].horizontalDirection);
		}

		_recordingIndex++;
	}
}
