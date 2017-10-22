using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
  
	private float _speed = 0.1f;
	private bool _recordPressed = false;
	private bool _recordWasPressed = false;

	private float _timeSinceLastShot = 0;
	private float _timeBetweenShots = 0.3f;

	private List<Vector3> _recordedMovement;
	private List<RecordedBullet> _recordedShots;

	private GameObject _cloneObject;
	private GameObject _bulletObject;

  void Start () {
		_recordedMovement = new List<Vector3>();
		_recordedShots = new List<RecordedBullet>();

		_cloneObject = Resources.Load("Clone") as GameObject;
		_bulletObject = Resources.Load("Bullet") as GameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_recordWasPressed = _recordPressed;
		_recordPressed = (Input.GetAxis("Jump") != 0);

		Move();
		Shoot();
		CreateClone();

		if (_timeSinceLastShot < _timeBetweenShots) {
			_timeSinceLastShot += Time.deltaTime;
			return;
		}
	}

	private void Shoot() {
		float horizontalShootInput = Input.GetAxis("ShootHorizontal");
		float verticalShootInput = Input.GetAxis("ShootVertical");

		if (_timeSinceLastShot >= _timeBetweenShots && (horizontalShootInput != 0 || verticalShootInput != 0)) {
			GameObject bullet = Instantiate(_bulletObject);
			bullet.transform.position = transform.position;
			bullet.GetComponent<BulletScript>().SetDirection(verticalShootInput, horizontalShootInput);

			_timeSinceLastShot = 0;

			if (_recordPressed) {
				_recordedShots.Add( new RecordedBullet { horizontalDirection = horizontalShootInput, verticalDirection = verticalShootInput, bullet = _bulletObject, shot = true } );
			}
		} else if (_recordPressed) {
			_recordedShots.Add ( new RecordedBullet { horizontalDirection = 0, verticalDirection = 0, bullet = _bulletObject, shot = false } );
		}
	}

	private void CreateClone() {
		if ( ! _recordPressed && _recordWasPressed) {
			GameObject instance = Instantiate(_cloneObject);
			
			instance.GetComponent<CloneScript>().SetMovement(_recordedMovement);
			instance.GetComponent<CloneScript>().SetBullets(_recordedShots);
			
			_recordedMovement = new List<Vector3>();
			_recordedShots = new List<RecordedBullet>();
		}
	}

	private void Move() {
		float verticalInput = Input.GetAxis("Vertical");

		if (verticalInput != 0) {
			transform.Translate(Vector3.forward * _speed * verticalInput);
		}

		float horizontalInput = Input.GetAxis("Horizontal");

		if (horizontalInput != 0) {
			transform.Translate(Vector3.right * _speed * horizontalInput);
		}

		if (_recordPressed) {
			_recordedMovement.Add(transform.position);
			return;
		} 
	}
}
