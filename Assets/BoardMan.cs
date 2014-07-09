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

	public void ExplodeTarget() {
		targetList[0].ExplosionGraphics();
		targetList.RemoveAt(0);
	}
	private bool IsTargetIntersectingOtherTargets(Target baseTarget) {
		foreach (Target target in targetList)
			if (baseTarget.IsTargetIntersecting(target))
				return true;
		return false;

	}

	private float randXWithinBounds() {
		float lowerBoundCircleX = Target.relativeCircleRadius;
		float upperBoundCircleX = BoardMan.widthScale - Target.relativeCircleRadius;
		return (float)((upperBoundCircleX - lowerBoundCircleX) * random.NextDouble() + lowerBoundCircleX);

	}
	private float randYWithinBounds() {
		float lowerBoundCircleY = Target.relativeCircleRadius;
		float upperBoundCircleY = BoardMan.heightScale - Target.relativeCircleRadius;
		return (float)((upperBoundCircleY - lowerBoundCircleY) * random.NextDouble() + lowerBoundCircleY);
	}
	public void AddTarget() {
		Target potentialTarget;
		do {
			potentialTarget = new Target(randXWithinBounds(), randYWithinBounds());
		} while (IsTargetIntersectingOtherTargets(potentialTarget));
		Target newTarget = new Target(potentialTarget.circleX, potentialTarget.circleY);
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