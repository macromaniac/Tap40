using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TweenInterface {
    private static UnityTween ensureTweenObjectExistsOnGameObject(GameObject gameObject) {
        UnityTween uTween = gameObject.GetComponent<UnityTween>();
        if (uTween == null) {
            uTween = gameObject.AddComponent<UnityTween>();
			uTween.initTweens();
        }
        return uTween;
    }
    public static void tweenToGameObject(GameObject gameObjectStart, GameObject gameObjectEndPosition,
            float delayInSeconds, float timeInSeconds, BaseTweenObject.OnTweenEnd onTweenEnd = null) {

        tweenMove(gameObjectStart,
            gameObjectEndPosition.transform.position, delayInSeconds, timeInSeconds, onTweenEnd);
    }
    public static void tweenDisplace(GameObject gameObjectStart, Vector3 displacement,
            float delayInSeconds, float timeInSeconds, BaseTweenObject.OnTweenEnd onTweenEnd = null) {

        tweenMove(gameObjectStart,
            gameObjectStart.transform.position + displacement, delayInSeconds, timeInSeconds, onTweenEnd);
    }

    public static void tweenMove(GameObject gameObjectStart, Vector3 endPosition,
            float delayInSeconds, float timeInSeconds, BaseTweenObject.OnTweenEnd onTweenEnd = null) {

        UnityTween uTween = ensureTweenObjectExistsOnGameObject(gameObjectStart);
        TweenPositionObject tpoEnd = new TweenPositionObject();
        tpoEnd.tweenValue = endPosition;
        tpoEnd.delay = delayInSeconds;
        tpoEnd.totalTime = timeInSeconds + delayInSeconds;
        tpoEnd.onTweenEnd = onTweenEnd;
        uTween.TweenPosition(tpoEnd);
    }
}
