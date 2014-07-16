using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class GameCalculator {

	List<InputFrame> keyframes;
	GameMan gameMan;
	public GameCalculator(System.Random random) {
		gameMan = new GameMan(random);
	}

	public bool AddKeyFrames(List<InputFrame> keyFrames) {
		try {
			foreach (InputFrame frame in keyFrames) {
				gameMan.AcceptInput(frame);
			}
		}
		catch {
			return false;
		}
		return true;
	}

	public GameState GetState() {
		return gameMan.gameState;
	}
	public float GetTime() {
		return gameMan.GetTime();
	}
	public int GetCircleOn() { // this function is used to keep track of the enemy
		return 0;
	}

	//NOTE: -1 = DID NOT FINISH
	// -2 = INCONSISTENCY
	public static float GetWinTime(System.Random random, List<InputFrame> keyFrames) {
		GameCalculator gameCalculator = new GameCalculator(Utility.DeepClone<System.Random>(random));
		if (!gameCalculator.AddKeyFrames(keyFrames))
			return -2f;
		if (gameCalculator.GetState() == GameState.Won)
			return gameCalculator.GetTime();
		return -1f;
	}
}
