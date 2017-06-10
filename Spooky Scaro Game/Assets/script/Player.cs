using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject cameraObject;

	public float speed;
	public float jump;

	float zFactor;
	float xFactor;

	void controls () {

		if (PlayerPrefs.GetInt ("stop") == 1) {
			if (Input.GetKey (KeyCode.W)) {
				zFactor = 1f;
			} else if (Input.GetKey (KeyCode.S)) {
				zFactor = -1f;
			} else {
				zFactor = 0f;
			}

			if (Input.GetKey (KeyCode.D)) {
				xFactor = 1f;
			} else if (Input.GetKey (KeyCode.A)) {
				xFactor = -1f;
			} else {
				xFactor = 0f;
			}
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			if (PlayerPrefs.GetInt ("stop") == 1) {
				PlayerPrefs.SetInt ("stop", 0);
			} else {
				PlayerPrefs.SetInt ("stop", 1);
			}
		}
			
		GetComponent <Rigidbody> ().velocity = cameraObject.transform.TransformDirection ( new Vector3 (speed * xFactor, GetComponent <Rigidbody> ().velocity.y, speed * zFactor) );



		if (Input.GetKeyDown (KeyCode.Space)) {
			GetComponent <Rigidbody> ().velocity = new Vector3 (GetComponent <Rigidbody> ().velocity.x, jump, GetComponent <Rigidbody> ().velocity.z);
		}


	}

	void cameraControl () {

		cameraObject.transform.position = transform.position; //+ new Vector3 (0, 10, -10);
		if (Input.GetKey (KeyCode.LeftArrow)) {
			cameraObject.transform.Rotate (new Vector3 (0f, -2f, 0f));
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			cameraObject.transform.Rotate (new Vector3 (0f, 2f, 0f));
		}

	}

	// Use this for initialization
	void Start () {

		PlayerPrefs.SetInt ("stop", 0);
	
		zFactor = 0f;
		xFactor = 0f;

	}
	
	// Update is called once per frame
	void Update () {

		cameraControl ();

		controls ();

	}
}
