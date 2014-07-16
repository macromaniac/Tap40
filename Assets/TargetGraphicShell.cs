using System.Collections;
using System.Collections.Generic;

public class TargetGraphicShell{
	protected float circleX, circleY;
	protected int targetNum;

	public TargetGraphicShell(float circleX, float circleY, int targetNum){
		this.circleX = circleX;
		this.circleY = circleY;
		this.targetNum = targetNum;
	}
	public virtual void AdvanceInactive(){
	}

	public virtual void AdvanceActiveState() {

	}
	public virtual void Explode() {

	}

}