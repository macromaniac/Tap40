using System.Collections;
using System.Collections.Generic;

using UnityEngine;
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

	private float randXWithinBounds(float spawnRadius) {
		float lowerBoundCircleX = spawnRadius;
		float upperBoundCircleX = BoardMan.widthScale - spawnRadius;
		return (float)((upperBoundCircleX - lowerBoundCircleX) * random.NextDouble() + lowerBoundCircleX);

	}
	private float randYWithinBounds(float spawnRadius) {
		float lowerBoundCircleY = spawnRadius;
		float upperBoundCircleY = BoardMan.heightScale - spawnRadius;
		return (float)((upperBoundCircleY - lowerBoundCircleY) * random.NextDouble() + lowerBoundCircleY);
	}
	public void AddTarget() {
		Target potentialTarget;
		do {
			potentialTarget =
				new Target(
					randXWithinBounds(Target.relativeCircleRadius),
					randYWithinBounds(Target.relativeCircleRadius),
					frameAt);
		} while (IsTargetIntersectingOtherTargets(potentialTarget));
		Target newTarget =
			new Target(potentialTarget.circleX, potentialTarget.circleY, frameAt);
		newTarget.makeGraphic();
		targetList.Add(newTarget);
	}
	public void AdvanceBoard() {
		foreach (Target target in targetList)
			target.TargetStateAdvance(frameAt);
	}

	public HitResponse processHit(float x, float y) {
		return targetList[0].GetHitResponse(frameAt, x, y);
	}
}