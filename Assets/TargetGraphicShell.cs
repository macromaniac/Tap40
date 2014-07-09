using System.Collections;
using System.Collections.Generic;

public class TargetGraphicShell{
	protected float circleX, circleY;

	public TargetGraphicShell(float circleX, float circleY){
		this.circleX = circleX;
		this.circleY = circleY;
	}
	public virtual void AdvanceInactive(){
	}

	public virtual void AdvanceActiveState(int state) {

	}
	public virtual void Explode() {

	}

}