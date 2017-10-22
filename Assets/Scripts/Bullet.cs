using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float speed = 0.5f;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate() {

        transform.position += transform.TransformDirection(Vector3.forward) * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyScript>() != null)
        {
            other.GetComponent<EnemyScript>().Hit();
            Destroy(gameObject);
        }
    }
}
