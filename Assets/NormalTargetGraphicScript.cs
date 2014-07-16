using UnityEngine;
using System.Collections;

public class NormalTargetGraphicScript : TargetGraphicScript {

	public GameObject activateFor10, activateFor20, activateFor30;

	public override void Init(int targetNum) {
		base.Init(targetNum);
		if (targetNum == 9)
			activateFor10.SetActive(true);
		if (targetNum == 19)
			activateFor20.SetActive(true);
		if (targetNum == 29)
			activateFor30.SetActive(true);
	}

	public override void IncrementState() {
		base.IncrementState();
	}
}
