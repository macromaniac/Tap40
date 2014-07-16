using UnityEngine;
using System.Collections;

public class TargetGraphicScript : MonoBehaviour {

	int currentState, targetNum;

	public void Init(int targetNum) {
		this.targetNum = targetNum;
		this.currentState = GameMan.numTargetsDisplayed + 1;
	}

	public void IncrementState() {
		currentState--;
	}
	
	void Update () {
	
	}
}
