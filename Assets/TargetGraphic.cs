using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetGraphic : TargetGraphicShell {
	GameObject target;
	TargetGraphicScript script;
	PointMapper pointMapper;
	float scale = 1f;
	int targetState = GameMan.numTargetsDisplayed + 1;

	private Vector3 UniversalCircleRadius {
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

	private Vector3 UniversalCircleScale {
		get {
			return new Vector3(UniversalCircleRadius.x * 2, UniversalCircleRadius.y * 2, 1f);
		}
	}

	private GameObject FindGameObjectToCopy() {
		return GameObject.Find("TargetBase");
	}

	private void PositionGraphic() {
		Vector3 centerVector = new Vector3(circleX, circleY, 0f);
		Vector3 pos = PointMapper.Get().GetWorldPointFromRelativePoint(centerVector);
		target.transform.position = pos;
	}

	private void UpdateScale() {
		target.transform.localScale = UniversalCircleScale * scale;
	}

	public TargetGraphic(float circleX, float circleY, int targetNum)
		: base(circleX, circleY, targetNum) {

			target = (GameObject)GameObject.Instantiate(Player.Get(0).GetInstantiableTarget());
			script = target.GetComponent<TargetGraphicScript>();
			script.Init(targetNum);
			target.SetActive(true);
			PositionGraphic();
			UpdateScale();
	}

	public override void AdvanceActiveState() {

		targetState--;
		script.IncrementState();

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
		UpdateScale();
	}
	public override void Explode() {
		GameObject.Destroy(target);
	}
}
