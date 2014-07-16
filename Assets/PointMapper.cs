using UnityEngine;
using System.Collections;


//On Touch print out relative coords [0-1,0-1.5]

public class PointMapper : MonoBehaviour {


	//NOTE: ROUNDING PREVENTS IT FROM BEING A TRUE INVERSION
	public Vector3 GetRelativePointFromWorldPoint(Vector3 worldPoint) {

		Vector3 relativeFromOrigin = worldPoint - gameObject.transform.position;

		float objWidth = gameObject.renderer.bounds.size.x;
		float objHeight = gameObject.renderer.bounds.size.y;
		return new Vector3(
			relativeFromOrigin.x / objWidth * BoardMan.widthScale,
			relativeFromOrigin.y / objHeight * BoardMan.heightScale,
			0f);
	}

	public Vector3 GetWorldPointFromRelativePoint(Vector3 relativePoint) {
		Vector3 unscaledRel = new Vector3(relativePoint.x / BoardMan.widthScale,
			relativePoint.y / BoardMan.heightScale,
			0f);
		Vector3 originInWorld = gameObject.transform.position;

		float objWidth = gameObject.renderer.bounds.size.x;
		float objHeight = gameObject.renderer.bounds.size.y;

		return new Vector3(
			originInWorld.x + unscaledRel.x * objWidth,
			originInWorld.y + unscaledRel.y * objHeight,
			0f);
	}

	static PointMapper pointMapper = null;
	public static PointMapper Get(){
		if (pointMapper == null)
			pointMapper = GameObject.Find("PointMapper").GetComponent<PointMapper>();
		return pointMapper;
	}
}
