using UnityEngine;
using System.Collections;
public class InputMan : MonoBehaviour {
	public PointMapper pointMapper;
	bool wasMousePressed = false;
	bool wasTouched = false;
	int frameAt = 0;

	System.Random baseRandom;
	public GameMan gameMan;

	void OnEnable() {
		System.Random rand = new System.Random();
		baseRandom = Utility.DeepClone<System.Random>(rand);
		gameMan = new GameMan(rand);
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
			Vector3 relPoint = pointMapper.GetRelativePointFromWorldPoint(worldPoint);
			Vector3 worldFromRelPoint = pointMapper.GetWorldPointFromRelativePoint(relPoint);
			gameMan.AcceptInput(new InputFrame(frameAt, relPoint.x, relPoint.y));
	}
	void FixedUpdate() {
		frameAt++;
		if (wasMousePressed) {
			wasMousePressed = false;
			processInput(Input.mousePosition);
			if (gameMan.gameState == GameState.Won) {
				float wTime = GameCalculator.getWinTime(baseRandom, gameMan.inputFrameList);
				Debug.Log(wTime);
			}
		}
		if (wasTouched) {
			wasTouched = false;
			processInput(touchedLast);
		}
	}
}
