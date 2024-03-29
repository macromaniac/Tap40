﻿using System.Collections;
using System.Collections.Generic;

public class BoardMan {
	private System.Random random;
	public static float widthScale = 1f;
	public static float heightScale = 1.5f;

	private List<Target> targetList;
	public int frameAt;

	public BoardMan(System.Random random) {
		targetList = new List<Target>();
		this.random = random;
	}

	public bool TryToExplodeTarget() {
		bool didExplode = targetList[0].TryToExplode();
		if (didExplode == true)
			targetList.RemoveAt(0);
		return didExplode;
	}
	private bool IsTargetIntersectingOtherTargets(Target baseTarget) {
		foreach (Target target in targetList)
			if (baseTarget.IsTargetSpawnIntersecting(target))
				return true;
		return false;
	}

	private float RandXWithinBounds() {
		float lowerBoundCircleX = Target.relativeCircleRadius;
		float upperBoundCircleX = BoardMan.widthScale - Target.relativeCircleRadius;
		return (float)((upperBoundCircleX - lowerBoundCircleX) * random.NextDouble() + lowerBoundCircleX);

	}
	private float RandYWithinBounds() {
		float lowerBoundCircleY = Target.relativeCircleRadius;
		float upperBoundCircleY = BoardMan.heightScale - Target.relativeCircleRadius;
		return (float)((upperBoundCircleY - lowerBoundCircleY) * random.NextDouble() + lowerBoundCircleY);
	}
	public void AddTarget(int targetNum) {
		Target potentialTarget;
		do {
			potentialTarget =
				new Target(
					RandXWithinBounds(),
					RandYWithinBounds(),
					frameAt, targetNum);
		} while (IsTargetIntersectingOtherTargets(potentialTarget));
		Target newTarget =
			new Target(potentialTarget.circleX, potentialTarget.circleY, frameAt,
				targetNum);
		newTarget.makeGraphic();
		targetList.Add(newTarget);
	}
	public void AdvanceBoard() {
		foreach (Target target in targetList)
			target.TargetStateAdvance(frameAt);
	}

	public HitResponse ProcessHit(float x, float y) {
		if (targetList.Count == 0)
			return HitResponse.Missed;
		return targetList[0].GetHitResponse(frameAt, x, y);
	}

	public int NumTargetsLeft() {
		return targetList.Count;
	}
}