using System;
using System.Collections;
using System.Collections.Generic;


public enum HitResponse { Hit, Missed, IsStillBeingLoaded };
public class Target {
	public static float relativeCircleRadius = 1f / (3.25f * 2f);

	private int delayFramesBeforeClickable = 5;
	public float circleX, circleY;
	protected int targetState;
	private int clickableAfterFrameNumber = 0;

	protected TargetGraphicShell targetGraphic;

	protected int frameSpawned;
	public Target(float x, float y, int frameSpawned) {
		this.circleX = x; this.circleY = y; this.frameSpawned = frameSpawned;
		targetState = GameMan.numTargetsDisplayed;
		targetGraphic = new TargetGraphicShell(circleX, circleY);
	}
	public virtual void makeGraphic() {
#if IS_SERVER
#else
		targetGraphic = new TargetGraphic(circleX, circleY);
#endif
		targetGraphic.AdvanceActiveState(targetState);
	}
	private bool IsPointWithinTarget(float pointX, float pointY) {
		//if the distance from the center of the circle to the point is
		//less than the radius then the point is within the target
		return (distance(pointX, pointY, circleX, circleY) < relativeCircleRadius);
	}

	public virtual bool IsTargetSpawnIntersecting(Target target) {
		return distance(target.circleX, target.circleY, circleX, circleY) < 2 * getAcceptableSpawnRadius();
	}
	protected float distance(float aX, float aY, float bX, float bY) {
		double distX = aX - bX;
		double distY = aY - bY;
		return (float)Math.Sqrt(distX * distX + distY * distY);
	}
	public HitResponse GetHitResponse(int frameAt, float pointX, float pointY) {
		if (IsPointWithinTarget(pointX, pointY) == true)
			if (clickableAfterFrameNumber < frameAt)
				return HitResponse.Hit;
			else
				return HitResponse.IsStillBeingLoaded;
		else {
			return HitResponse.Missed;
		}
	}

	public void TargetStateAdvance(int frameAt) {
		targetState--;
		if (targetState == 0)
			clickableAfterFrameNumber = frameAt + delayFramesBeforeClickable;
		UpdateTargetGraphic();
	}
	public void UpdateTargetGraphic() {
		targetGraphic.AdvanceActiveState(targetState);
	}
	public bool TryToExplode() {
		targetGraphic.Explode();
		return true;
	}
	public float getAcceptableSpawnRadius() {
		return relativeCircleRadius;
	}
}