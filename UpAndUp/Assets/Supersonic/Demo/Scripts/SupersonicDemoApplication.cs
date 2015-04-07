using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SupersonicDemoApplication : MonoBehaviour
{


	//Supersonic's Application Key
	public string applicationKey = "APP Key";
	//The Unique user ID of the current session
	public string userId = "USER ID";
	//Flag for RV Availability that is set using the events (See below)

	bool isRVHasVideo = false;
	//Flag for IS Availability
	bool isISAvailable = false;
	//Misc stuff for scrolling
	Vector2 scrollPosition;
	Touch touch;
	//multiplier for buttons size
	private float multiplier = 1f;
	//Last event received for the event label
	private string _lastEvent = "";
	//Current credits
	private int _credits = 0;


	void Start ()
	{
		if(Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
			Debug.Log("Supersonic SDK Is only supported in an iOS device or an Android device, and will not work on the Unity Simulator");

		//Init Supersonic
		Supersonic.init ();

		//Adjust the "multiplier" value to adjust buttons size
		if (Screen.width < Screen.height)
			multiplier = (float)((double)Screen.width / 640);
		else
			multiplier = (float)((double)Screen.height / 640);

		//Initialize RewardedVideo
		Supersonic.initRewardedVideo (applicationKey, userId);

		//Initialize Interstitial
		Supersonic.initInterstitial (applicationKey, userId);

	}

	void OnGUI ()
	{  	
		scrollPosition = GUI.BeginScrollView (new Rect (0, 0, Screen.width, Screen.height), scrollPosition, new Rect (0, 0, Screen.width + 1000f, Screen.height + 1000f), GUIStyle.none, GUIStyle.none);
		//Show offerwall when the user clicks the button
		GUIStyle btnStyle = GUI.skin.button;
		btnStyle.fontSize = 50;
		GUI.backgroundColor = Color.black;
		
		
		//Show the Offerwall
		if (GUI.Button (getRect (50, 50, 500, 100), "OfferWall", btnStyle)) {
			//Use client side callbacks, you can also skip this step and only call Supersonic.showOfferWall(applicationKey, userId) without the dictionary
			Dictionary<string,object> dict = new Dictionary<string,object>();
			dict.Add("useClientSideCallbacks", "true");
			Supersonic.showOfferWall (applicationKey, userId, dict);	
		}
		
		//If Interstitial is not available, Disable the Interstitial button
		if (!isISAvailable)
			GUI.enabled = false;
		//Show the Interstitial
		if (GUI.Button(getRect(50,160,500,100),"Interstitial",btnStyle)) {
			Supersonic.showInterstitial();
		}
		GUI.enabled = true;
		//If the onRVNoMoreOffers event was fired, disable the video button
		//if the onRVInitSuccess was yet to be called or onRVInitFail event was fired, disable the video button
		if (!isRVHasVideo)
			GUI.enabled = false;
		//If the onRVInitSuccess event was fired, you can show a video
		if (GUI.Button(getRect(50,270,500,100),"RewardedVideo",btnStyle)) {
			if(isRVHasVideo)
				Supersonic.showRewardedVideo();
			
		}
		GUI.enabled = true;
		GUI.skin.label.fontSize = 50;
		
		GUI.Label (getRect(10,380,1000,500), "Last event: " + _lastEvent);
		GUI.Label (getRect(10,500,1000,500), "Credits: " + _credits);
		GUI.EndScrollView ();
		
	}

	void OnApplicationPause (bool pauseStatus)
	{
		
		if (pauseStatus)
			Supersonic.onPause ();
		else
			Supersonic.onResume ();
		
	}

	void OnEnable ()
	{
		Debug.Log ("OnEnable");
		//Listen to All events just for illustration purpose (You don't have to listen to all events or any events if you don't want to)
		/*Offerwall*/
		SupersonicEvents.onOWShowSuccessEvent += onOWShowSuccessEvent;
		SupersonicEvents.onGetOWCreditsFailedEvent += onGetOWCreditsFailedEvent;
		SupersonicEvents.onOWAdCreditedEvent += onOWAdCreditedEvent;
		SupersonicEvents.onOWShowFailEvent += onOWShowFailEvent;
		SupersonicEvents.onOWDidCloseEvent += onOWDidCloseEvent;
		
		/*RewardedVideo*/
		SupersonicEvents.onRVNoMoreOffersEvent += onRVNoMoreOffersEvent;
		SupersonicEvents.onRVAdCreditedEvent += onRVAdCreditedEvent;
		SupersonicEvents.onRVInitFailEvent += onRVInitFailEvent;
		SupersonicEvents.onRVInitSuccessEvent += onRVInitSuccessEvent;
		SupersonicEvents.onRVShowFailEvent += onRVShowFailEvent;
		SupersonicEvents.onRVWillOpenEvent += onRVWillOpenEvent;
		SupersonicEvents.onRVDidCloseEvent += onRVDidCloseEvent;
		
		/*Interstitial*/
		SupersonicEvents.onISInitSuccessEvent += onISInitSuccessEvent;
		SupersonicEvents.onISInitFailEvent += onISInitFailEvent;
		SupersonicEvents.onISAvailabilityEvent += onISAvailabilityEvent;
		SupersonicEvents.onISShowSuccessEvent += onISShowSuccessEvent;
		SupersonicEvents.onISDidCloseEvent +=onISDidCloseEvent;
		SupersonicEvents.onISShowFailEvent +=onISShowFailEvent;
		SupersonicEvents.onISAdClickedEvent +=onISAdClickedEvent;
	}
	
	/* OfferWall */
	void onOWShowSuccessEvent ()
	{
		Debug.Log ("onOWShowSuccessEvent");
		_lastEvent = "onOWShowSuccessEvent";
	}
	void onOWDidCloseEvent ()
	{
		Debug.Log ("onOWDidCloseEvent");
		_lastEvent = "onOWDidCloseEvent";
	}
	void onOWShowFailEvent (string val)
	{
		Debug.Log ("onOWShowFailEvent : " + val);
		_lastEvent = "onOWShowFailEvent: " + val;
	}
	void onGetOWCreditsFailedEvent (string val)
	{
		Debug.Log ("onGetOWCreditsFailedEvent: " + val);
		_lastEvent = "onGetOWCreditsFailedEvent: " + val;
	}
	
	void onOWAdCreditedEvent (Dictionary<string,object> dict)
	{
		//Get the amount of credits the user should be awarded of from the dictionary
		object temp = null;
		dict.TryGetValue ("credits", out temp);
		int credits = int.Parse(temp.ToString());
		_credits += credits;
		Debug.Log ("onOWAdCreditedEvent");
		_lastEvent = "onOWAdCreditedEvent: " + credits;
	}
	
	/* RewardedVideo */
	void onRVNoMoreOffersEvent ()
	{
		Debug.Log ("onRVNoMoreOffersEvent");
		isRVHasVideo = false;
		_lastEvent = "onRVNoMoreOffersEvent";
	}
	void onRVWillOpenEvent ()
	{
		Debug.Log ("onRVWillOpenEvent");
		_lastEvent = "onRVWillOpenEvent";
	}
	void onRVDidCloseEvent ()
	{
		Debug.Log ("onRVDidCloseEvent");
		_lastEvent = "onRVDidCloseEvent";
	}
	void onRVShowFailEvent (string val)
	{
		Debug.Log ("onRVShowFailEvent" + val);
		_lastEvent = "onRVShowFailEvent: " + val;
	}
	void onRVAdCreditedEvent (int credits)
	{
		Debug.Log ("onRVAdCreditedEvent" + credits);
		_lastEvent = "onRVAdCreditedEvent: " + credits;
		_credits += credits;
	}
	
	void onRVInitFailEvent (string val)
	{
		Debug.Log ("onRVInitFailEvent : " + val);	
		isRVHasVideo = false;
		_lastEvent = "onRVInitSuccessEvent: " + val;
	}
	void onRVInitSuccessEvent (Dictionary<string,object> dict)
	{
		Debug.Log ("onRVInitSuccessEvent");
		isRVHasVideo = true;
		_lastEvent = "onRVInitSuccessEvent";
	}

	/* Interstitial */
	void onISInitSuccessEvent ()
	{
		Debug.Log ("onISInitSuccessEvent");
		_lastEvent = "onISInitSuccessEvent";
	}
	void onISInitFailEvent (string val)
	{
		Debug.Log ("onISInitFailEvent : " + val);
		_lastEvent = "onISInitFailEvent: " + val;
	}
	void onISAvailabilityEvent(bool available){
		Debug.Log ("onISAvailabilityEvent: " + available.ToString());
		_lastEvent = "onISAvailabilityEvent: " + available.ToString();
		isISAvailable = available;
	}
	void onISShowSuccessEvent(){
		Debug.Log ("onISShowSuccessEvent");
		_lastEvent = "onISShowSuccessEvent";
	}
	void onISShowFailEvent (string val)
	{
		Debug.Log ("onISShowFailEvent: " + val);
		_lastEvent = "onISShowFailEvent: " + val;
	}

	void onISDidCloseEvent ()
	{
		Debug.Log ("onISDidCloseEvent");
		_lastEvent = "onISDidCloseEvent";
	}

	void onISAdClickedEvent(){

		Debug.Log ("onISAdClickedEvent");
		_lastEvent = "onISAdClickedEvent";
	}


	void OnDisable ()
	{
		//Stop listening to the events after the specific gameobject is destroy
		
		/*Offerwall*/
		SupersonicEvents.onOWShowSuccessEvent -= onOWShowSuccessEvent;
		SupersonicEvents.onGetOWCreditsFailedEvent -= onGetOWCreditsFailedEvent;
		SupersonicEvents.onOWAdCreditedEvent -= onOWAdCreditedEvent;
		SupersonicEvents.onOWShowFailEvent -= onOWShowFailEvent;
		SupersonicEvents.onOWDidCloseEvent -= onOWDidCloseEvent;
		
		/*RewardedVideo*/
		SupersonicEvents.onRVNoMoreOffersEvent -= onRVNoMoreOffersEvent;
		SupersonicEvents.onRVAdCreditedEvent -= onRVAdCreditedEvent;
		SupersonicEvents.onRVInitFailEvent -= onRVInitFailEvent;
		SupersonicEvents.onRVInitSuccessEvent -= onRVInitSuccessEvent;
		SupersonicEvents.onRVShowFailEvent -= onRVShowFailEvent;
		SupersonicEvents.onRVWillOpenEvent -= onRVWillOpenEvent;
		SupersonicEvents.onRVDidCloseEvent -= onRVDidCloseEvent;
		
		/*Interstitial*/
		SupersonicEvents.onISInitSuccessEvent -= onISInitSuccessEvent;
		SupersonicEvents.onISInitFailEvent -= onISInitFailEvent;
		SupersonicEvents.onISDidCloseEvent -=onISDidCloseEvent;
		SupersonicEvents.onISAvailabilityEvent -= onISAvailabilityEvent;
		SupersonicEvents.onISShowFailEvent -=onISShowFailEvent;
		
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.touchCount > 0) {
			touch = Input.touches [0];
			if (touch.phase == TouchPhase.Moved) {
				if(touch.deltaPosition.normalized.x>0||touch.deltaPosition.normalized.x<0)
					scrollPosition.x -= touch.deltaPosition.x*2f;
				if(touch.deltaPosition.normalized.y>0||touch.deltaPosition.normalized.y<0)
					scrollPosition.y += touch.deltaPosition.y*2f;
			}
		}
	}
	

	

	Rect getRect (float l, float t, float w, float h)
	{
		return new Rect (l * multiplier, t * multiplier, w * multiplier, h * multiplier);
	}


}

