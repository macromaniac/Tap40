using System;
using System.Collections.Generic;



public enum GameState { Playing, Won, Lost, Disconnected };

public class InputFrame {
	public float x, y;
	public bool hasInput;
	public int frameNumber;
	public InputFrame(int frameNumber) {
		this.frameNumber = frameNumber; x = 0; y = 0; hasInput = false;
	}
	public InputFrame(int frameNumber, float x, float y) {
		this.frameNumber = frameNumber; this.x = x; this.y = y; hasInput = true;
	}
}
public class GameMan {


	private static int numTargetsStarting = 30;
	private int numTargetsLeft = numTargetsStarting;
	private BoardMan boardMan;

	public static int numTargetsDisplayed = 3;
	public GameState gameState;

	public List<InputFrame> inputFrameList;

	private void AddTargetToBoard() {
		boardMan.AddTarget(numTargetsStarting - numTargetsLeft);
		numTargetsLeft--;
	}
	public GameMan(System.Random random) {
		inputFrameList = new List<InputFrame>();
		boardMan = new BoardMan(random);
		for (int i = 0; i < numTargetsDisplayed; ++i) {
			boardMan.AdvanceBoard();
			AddTargetToBoard();
		}
		gameState = GameState.Playing;
	}

	public void AcceptInput(InputFrame inputFrame) {
		inputFrameList.Add(inputFrame);
		//if (gameState != GameState.Playing)
		//	return;

		if (inputFrame.frameNumber < 0 || inputFrame.frameNumber < boardMan.frameAt)
			throw new System.ArgumentException("Invalid FrameNumber");

		boardMan.frameAt = inputFrame.frameNumber;

		if (inputFrame.hasInput) {
			HitResponse resp = boardMan.ProcessHit(inputFrame.x, inputFrame.y);
			if (resp == HitResponse.Missed) {
				gameState = GameState.Lost;
			}
			if (resp == HitResponse.Hit) {
				if (boardMan.TryToExplodeTarget()) {
					boardMan.AdvanceBoard();
					if (numTargetsLeft > 0) {
						AddTargetToBoard();
					}
				}
				if (boardMan.NumTargetsLeft() == 0)
					gameState = GameState.Won;
			}
		}
	}

	float frameLength = 0.02f;
	public float GetTime() {
		return boardMan.frameAt * frameLength;
	}

}
