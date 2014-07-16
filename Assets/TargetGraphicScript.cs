using UnityEngine;
using System.Collections;

public class TargetGraphicScript : MonoBehaviour {

	protected int currentState, targetNum;

	public virtual void Init(int targetNum) {
		this.targetNum = targetNum;
		this.currentState = GameMan.numTargetsDisplayed + 1;
	}

	public virtual void IncrementState() {
		currentState--;
	}
	
	void Update () {
	
	}
}
