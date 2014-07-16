using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetGraphic : TargetGraphicShell {
	GameObject target;
	PointMapper pointMapper;
	float scale = 1f;

	private Vector3 universalCircleRadius {
		get {
			PointMapper mapper = PointMapper.Get();
			// X/Y Edge of a circle at Relative Origin
			Vector3 circleXYEdgePosition =
				mapper.GetWorldPointFromRelativePoint(new Vector3(Target.relativeCircleRadius,
					Target.relativeCircleRadius, 0f));

			//Center of a circle at Relative Origin
			Vector3 circleCenterPosition =
				mapper.GetWorldPointFromRelativePoint(new Vector3(0f, 0f, 0f));

			//return radius of circle
			return (circleXYEdgePosition - circleCenterPosition);
		}
	}

	private Vector3 universalCircleScale {
		get {
			return new Vector3(universalCircleRadius.x * 2, universalCircleRadius.y * 2, 1f);
		}
	}

	private GameObject findGameObjectToCopy() {
		return GameObject.Find("TargetBase");
	}

	private void positionGraphic() {
		Vector3 centerVector = new Vector3(circleX, circleY, 0f);
		Vector3 pos = PointMapper.Get().GetWorldPointFromRelativePoint(centerVector);
		target.transform.position = pos;
	}

	private void updateScale() {
		target.transform.localScale = universalCircleScale * scale;
	}

	public TargetGraphic(float circleX, float circleY)
		: base(circleX, circleY) {

		GameObject targetGraphicBase = findGameObjectToCopy();
		target = (GameObject)GameObject.Instantiate(targetGraphicBase);
		target.SetActive(true);
		positionGraphic();
		updateScale();
	}

	public override void AdvanceActiveState(int targetState) {

		if (targetState == 1) {
			target.GetComponent<SpriteRenderer>().color = Color.red;
			scale = .95f;
		}
		if (targetState == 2) {
			target.GetComponent<SpriteRenderer>().color = Color.yellow;
			scale = .6f;
		}
		if (targetState == 3) {
			target.GetComponent<SpriteRenderer>().color = Color.grey;
			scale = .3f;
		}
		if (targetState == 4) {
			target.GetComponent<SpriteRenderer>().color = Color.white;
			scale = .2f;
		}
		updateScale();
	}
	public override void Explode() {
		GameObject.Destroy(target);
	}
}
