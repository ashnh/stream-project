using UnityEngine;
using System.Collections;

public class StopableObject : MonoBehaviour {



	// user inputed values
	public string routine;
	public string pairedValues;
	public bool repeat;



	// stage movement
	string currentStage;
	int currentStageNumber;
	bool inProgress;
	Vector3 startPosition;
	string[] sequence () {
		return routine.Split (';');
	}
	string[] pairedSequence () {
		return pairedValues.Split (';');
	}
	void moveSequence () {
		if (sequence ().Length - 1 >= currentStageNumber) {
			currentStageNumber++;
			currentStage = sequence () [currentStageNumber];
		}
	}
	void resetSequence () {
		currentStageNumber = 0;
		currentStage = sequence () [currentStageNumber];
	}



	// stages
	bool moveForward (string stage, float edge, float startPosition, float speed) {
		bool isOver;
		if (speed >= 0) {
			isOver = /*over condition*/ !(transform.position.z < edge + startPosition);
		} else {
			isOver = /*over condition*/ !(transform.position.z > edge + startPosition);
		}
		if (stage == "moveForward") {
			if (!isOver) {
			// sequence
				GetComponent <Rigidbody> ().velocity = new Vector3 (GetComponent <Rigidbody> ().velocity.x, GetComponent <Rigidbody> ().velocity.y, speed);
				inProgress = true;
			} else {
				inProgress = false;
				GetComponent <Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			}
		}
		return isOver;
	}
	bool moveSideways (string stage, float edge, float startPosition, float speed) {
		bool isOver;
		if (speed >= 0) {
			isOver = /*over condition*/ !(transform.position.x < edge + startPosition);
		} else {
			isOver = /*over condition*/ !(transform.position.x > edge + startPosition);
		}
		if (stage == "moveSideways") {
			if (!isOver) {
				// sequence
				GetComponent <Rigidbody> ().velocity = new Vector3 (speed, GetComponent <Rigidbody> ().velocity.y, GetComponent <Rigidbody> ().velocity.z);
				inProgress = true;
			} else {
				inProgress = false;
				GetComponent <Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			}
		}
		return isOver;
	}



	// routine execution
	void processSteps () {
		if (!inProgress) {
			startPosition = transform.position;
		}
		switch (currentStage) {
		case "moveForward":
			if (moveForward (currentStage, float.Parse(pairedSequence ()[currentStageNumber].Split (',')[0]) , startPosition.z, float.Parse(pairedSequence ()[currentStageNumber].Split (',')[1]))) {
				moveSequence ();
				GetComponent <Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			}
			break;
		case "moveSideways":
			if (moveSideways (currentStage, float.Parse(pairedSequence ()[currentStageNumber].Split (',')[0]) , startPosition.z, float.Parse(pairedSequence ()[currentStageNumber].Split (',')[1]))) {
				moveSequence ();
				GetComponent <Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			}
			break;
		case "stop":
		default:
			if (!repeat) {
				if (!inProgress) {
					GetComponent <Rigidbody> ().velocity = new Vector3 (0, 0, 0);
					inProgress = true;
				}
			} else {
				resetSequence ();
			}
			break;
		}
		Debug.Log (currentStage);
	}



	// initialization
	void Start () {
		currentStageNumber = 0;
		currentStage = sequence () [currentStageNumber];
		inProgress = false;
	}


	
	// routine execution call
	void Update () {
		if (PlayerPrefs.GetInt ("stop") == 0) {
			processSteps ();
		}
	}


}


