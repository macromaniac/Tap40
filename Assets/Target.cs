using System;
using System.Collections;
using System.Collections.Generic;


public enum HitResponse { Hit, Missed, IsStillBeingLoaded };
public class Target {
	public static float relativeCircleRadius = 1f / (5f * 2f);

	private int delayFramesBeforeClickable = 5;
	public float circleX, circleY;
	protected int targetState;
	private int clickableAfterFrameNumber = 0;
	public Target(float x, float y) {
		targetState = GameMan.numTargetsDisplayed;
		this.circleX = x; this.circleY = y;
	}
	private bool IsPointWithinTarget(float pointX, float pointY) {
		//if the distance from the center of the circle to the point is
		//less than the radius then the point is within the target
		return (distance(pointX,pointY,circleX,circleY) < relativeCircleRadius);
	}

	public bool IsTargetIntersecting(Target target) {
		return distance(target.circleX, target.circleY, circleX, circleY) < 2 * relativeCircleRadius;
	}
	private float distance(float aX, float aY, float bX, float bY) {
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
	protected virtual void UpdateTargetGraphic() { }
	public virtual void ExplosionGraphics() { }
}