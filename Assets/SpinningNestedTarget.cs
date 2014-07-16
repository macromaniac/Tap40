using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpinningNestedTarget : Target {

	public static float paddingBetweenCircles = .1f;
	private int lastFrameHit = 0;

	private int framesInARotation = 400;
	protected TargetGraphicShell secondGraphic;

	enum SpinningNestedState { Spinning, Spawned };
	private SpinningNestedState spinningNestedState = SpinningNestedState.Spinning;

	public SpinningNestedTarget(float x, float y, int frameSpawned)
		: base(x, y, frameSpawned) {
	}

	public override void makeGraphic() {
		if (spinningNestedState == SpinningNestedState.Spinning) {
#if IS_SERVER
#else
			targetGraphic = new TargetGraphic(circleX, circleY);
#endif
			targetGraphic.AdvanceActiveState(targetState);
		}
		else {
#if IS_SERVER
		secondGraphic = new TargetGraphicShell(circleX,circleY);
#else
			secondGraphic = new TargetGraphic(circleX, circleY);
#endif
		}
	}

	public override HitResponse GetHitResponse(int frameAt, float pointX, float pointY) {
		lastFrameHit = frameAt;
		return base.GetHitResponse(frameAt, pointX, pointY);
	}

	private float getCurrentDegree(int frame) {
		int framesAlive = frame - this.frameSpawned;
		float toReturn =  ((float)framesAlive) * 360f / ((float)framesInARotation);
		Debug.Log(toReturn);
		return toReturn;
	}

	private void generateNextCircleXY() {
		//in relative distance
		float totalDistanceToNextCircle = paddingBetweenCircles + relativeCircleRadius * 2;

		double curDegInRadians = (Math.PI) / 180 * (double)getCurrentDegree(lastFrameHit);
		float nextCircleXDelta = totalDistanceToNextCircle * (float)Math.Cos(curDegInRadians);
		float nextCircleYDelta = totalDistanceToNextCircle * (float)Math.Sin(curDegInRadians);

		circleX += nextCircleXDelta;
		float maxCircleX = BoardMan.widthScale - Target.relativeCircleRadius;
		float minCircleX = Target.relativeCircleRadius;
		if (circleX > maxCircleX) circleX = maxCircleX;
		if (circleX < minCircleX) circleX = minCircleX;

		circleY += nextCircleYDelta;
		float maxCircleY = BoardMan.heightScale - Target.relativeCircleRadius;
		float minCircleY = Target.relativeCircleRadius;
		if (circleY > maxCircleY) circleY = maxCircleY;
		if (circleY < minCircleY) circleY = minCircleY;
	}

	private void spawnNextCircle() {
		spinningNestedState = SpinningNestedState.Spawned;
		generateNextCircleXY();
		makeGraphic();
	}

	public override bool TryToExplode() {
		if (spinningNestedState == SpinningNestedState.Spinning) {
			targetGraphic.Explode();
			spawnNextCircle();
			return false;
		}
		else {
			secondGraphic.Explode();
			return true;
		}
	}
	public override float getAcceptableSpawnRadius() {
		return paddingBetweenCircles + relativeCircleRadius * 2;
	}
}