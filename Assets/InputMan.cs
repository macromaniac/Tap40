using UnityEngine;
using System.Collections;

public class InputMan : MonoBehaviour {
	public PointMapper pointMapper;
	bool wasMousePressed = false;
	bool wasTouched = false;
	int frameAt = 0;

	public GameMan gameMan;

	void OnEnable() {
		gameMan = new GameMan(new System.Random());
	}

	private Vector3 touchedLast;
	void Update() {
		if (Input.GetMouseButtonDown(0))
			wasMousePressed = true;
		if (Input.touchCount > 0) {
			wasTouched = true;
			touchedLast = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 0f);
		}

	}

	private void processInput(Vector3 input) {
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(input);
			Vector3 relPoint = pointMapper.getRelativePointFromWorldPoint(worldPoint);
			Vector3 worldFromRelPoint = pointMapper.getWorldPointFromRelativePoint(relPoint);
			gameMan.acceptInput(new InputFrame(frameAt, relPoint.x, relPoint.y));
	}
	void FixedUpdate() {
		frameAt++;
		if (wasMousePressed) {
			wasMousePressed = false;
			processInput(Input.mousePosition);
		}
		if (wasTouched) {
			wasTouched = false;
			processInput(touchedLast);
		}
	}
}
