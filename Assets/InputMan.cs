using UnityEngine;
using System.Collections;

public class InputMan : MonoBehaviour {
	public PointMapper pointMapper;
	bool wasMousePressed = false;
	int frameAt = 0;

	public GameMan gameMan;

	void OnEnable() {
		gameMan = new GameMan(new System.Random());
	}
	void Update() {
		if (Input.GetMouseButtonDown(0))
			wasMousePressed = true;
	}
	void FixedUpdate() {
		frameAt++;
		if (wasMousePressed) {
			wasMousePressed = false;
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 relPoint = pointMapper.getRelativePointFromWorldPoint(worldPoint);
			Vector3 worldFromRelPoint = pointMapper.getWorldPointFromRelativePoint(relPoint);
			gameMan.acceptInput(new InputFrame(frameAt, relPoint.x, relPoint.y));
		}
	}
}
