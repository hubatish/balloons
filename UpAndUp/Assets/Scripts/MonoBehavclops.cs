using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MonoBehavclops : MonoBehaviour 
{

	public static bool DoImmediately = true;
	public static bool DoAfterFirstInterval = false;
	public static float Forever = -1f;
	public static float NoVariance = 0f;
	
	public delegate void RepeatAction();
	public delegate void TweenAction(float t);

	private Dictionary<Type,Component> _cachedComponents;

	private GameObject _gameObject;
	public GameObject thisGameObject
	{
		get
		{
			if (_gameObject == null) _gameObject = gameObject;
			return _gameObject;
		}
	}

	public T Cached<T>() where T : Component
	{
		if (_cachedComponents == null) _cachedComponents = new Dictionary<Type,Component>();
		if (_cachedComponents.ContainsKey(typeof(T))) return (T)_cachedComponents[typeof(T)];
		T component = GetComponent<T>();
		_cachedComponents.Add(typeof(T),component);
		return component;
	}

	protected void Delay(float delay, Action action)
	{
		if (!thisGameObject.activeSelf) return;
		StartCoroutine(DelayEnumerator(delay, action));
	}

	private IEnumerator DelayEnumerator(float delay, Action action)
	{
		yield return new WaitForSeconds(delay);
		action();
	}


	protected void Repeat(float interval, RepeatAction action) 
	{ 
		Repeat(interval, NoVariance, Forever, DoImmediately, action); 
	}

	protected void Repeat(float interval, float variance, RepeatAction action) 
	{
		Repeat(interval, variance, Forever, DoImmediately, action); 
	}
	
	protected void Repeat(float interval, bool doImmediately, RepeatAction action) 
	{
		Repeat(interval, NoVariance, Forever, doImmediately, action); 
	}

	protected void Repeat(float interval, float variance, bool doImmediately, RepeatAction action) 
	{ 
		Repeat(interval, variance, Forever, doImmediately, action); 
	}

	protected void Repeat(float interval, float variance, float duration, bool doImmediately, RepeatAction action)
	{
		if (!thisGameObject.activeSelf) return;
		StartCoroutine(RepeatEnumerator(interval, variance, duration, doImmediately, action));
	}

	private IEnumerator RepeatEnumerator(float interval, float variance, float duration, bool doImmediately, RepeatAction action)
	{
		if (doImmediately) action();
		float startTime = Time.time;
		float variedInterval = interval;
		float minInterval = Mathf.Max(interval - Mathf.Abs(variance), 0f);
		float maxInterval = interval + Mathf.Abs(variance);
		while (duration < 0f || Time.time - startTime <= duration) {
			if (variance != 0f) variedInterval = UnityEngine.Random.Range(minInterval, maxInterval);
			yield return new WaitForSeconds(variedInterval);
			action();
		}
	}

	// TODO: add ease types
	protected void Tween(float duration, Action<float> action)
	{
		Tween(duration, false, action);
	}

	protected void Tween(float duration, bool smoothed, Action<float> action)
	{
		if (!gameObject.activeSelf) return;
		if (duration <= 0f) return;
		StartCoroutine(TweenEnumerator(duration, action, smoothed));
	}

	private IEnumerator TweenEnumerator(float duration, Action<float> action, bool smoothed = false)
	{
		float time = 0f;
		while (time < duration) {
			float t = time/duration;
			if (smoothed) t = t * t * (3 - 2 * t);
			action(t);
			time += Time.deltaTime;
			yield return null;
		}
		action(1f);
	}
}
