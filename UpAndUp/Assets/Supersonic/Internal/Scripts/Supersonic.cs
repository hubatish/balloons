using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class Supersonic
{

#if UNITY_IPHONE
	//Import all Supersonic methods from the C side of the Supersonic script

	/*General*/
	[DllImport("__Internal")]
	private static extern void _supersonicInit();
	[DllImport("__Internal")]
	private static extern void _supersonicInitTestInstance();

	/*Advertiser*/
	[DllImport("__Internal")]
	private static extern void _supersonicReportAppStarted();

	/*Offerwall*/
	[DllImport("__Internal")]
	private static extern void _supersonicShowOfferWall( string applicationKey, string userId, string additionalParameters );
	[DllImport("__Internal")]
	private static extern void _supersonicGetOfferWallCredits( string applicationKey, string userId);

	/*RewardedVideo*/
	[DllImport("__Internal")]
	private static extern void _supersonicShowRewardedVideo();
	[DllImport("__Internal")]
	private static extern void _supersonicInitRewardedVideo(string applicationKey, string userId, string additionalParameters);

	/*Interstitial*/
	[DllImport("__Internal")]
	private static extern void _supersonicInitInterstitial(string applicationKey, string userId, string additionalParameters);
	[DllImport("__Internal")]
	private static extern bool _supersonicIsInterstitialAdAvailable();
	[DllImport("__Internal")]
	private static extern void _supersonicShowInterstitial();
	[DllImport("__Internal")]
	private static extern void _supersonicForceShowInterstitial();


	/*General*/
	public static void init(){
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			_supersonicInit ();
	}

	public static void initTestInstance(){
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			_supersonicInitTestInstance ();
	}

	///Here just to support crossplatform compile ( Android support this functions only )
	public static void release(){}
	public static void onResume(){}
	public static void onPause(){}
	////////////////////////////////
	
	/*Advertiser*/
	public static void reportAppStarted()
	{
		
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_supersonicReportAppStarted();
		
	}

	/*Offerwall API*/
	public static void showOfferWall( string applicationKey, string userId)
	{
		showOfferWall (applicationKey, userId, null);
	}

	public static void showOfferWall( string applicationKey, string userId, Dictionary<string,object> additionalParameters )
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Dictionary<string,object> dict = additionalParameters;
			if (dict==null)
				dict = new Dictionary<string,object>();
			dict.Add("SDKPluginType","Unity");
			_supersonicShowOfferWall (applicationKey, userId, dict != null ? MiniJSON.Json.Serialize (dict) : null);
		}
	}

	public static void getOfferWallCredits( string applicationKey, string userId )
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_supersonicGetOfferWallCredits( applicationKey, userId);
	}

	/*RewardedVideo API*/
	public static void initRewardedVideo(string applicationKey, string userId)
	{
		initRewardedVideo (applicationKey,userId,null);
	}

	public static void initRewardedVideo(string applicationKey, string userId, Dictionary<string,object> additionalParameters)
	{

		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Dictionary<string,object> dict = additionalParameters;
			if (dict==null)
				dict = new Dictionary<string,object>();
			dict.Add("SDKPluginType","Unity");
			_supersonicInitRewardedVideo (applicationKey, userId, dict != null ? MiniJSON.Json.Serialize (dict) : null);
		}
		
	}

	public static void showRewardedVideo()
	{

		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_supersonicShowRewardedVideo();

	}

	/*Interstitial API*/
	public static void initInterstitial(string applicationKey, string userId)
	{
		initInterstitial (applicationKey,userId,null);
	}

	public static void initInterstitial(string applicationKey, string userId, Dictionary<string,object> additionalParameters)
	{
		
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Dictionary<string,object> dict = additionalParameters;
			if (dict==null)
				dict = new Dictionary<string,object>();
			dict.Add("SDKPluginType","Unity");
			_supersonicInitInterstitial (applicationKey, userId, dict != null ? MiniJSON.Json.Serialize (dict) : null);
		}
		
	}

	public static bool isInterstitialAdAvailable(){
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			return _supersonicIsInterstitialAdAvailable ();
		return false;
	}

	public static void showInterstitial()
	{
		
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_supersonicShowInterstitial();
		
	}
	public static void forceShowInterstitial()
	{
		
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_supersonicForceShowInterstitial();
		
	}

#endif
#if UNITY_ANDROID

	private static AndroidJavaObject _plugin;
	private readonly static string SUPERSONIC_PLUGIN_PACKAGE = "com.supersonic.SupersonicPlugin";

	public static void init()
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		using( var pluginClass = new AndroidJavaClass( SUPERSONIC_PLUGIN_PACKAGE ) )
			_plugin = pluginClass.CallStatic<AndroidJavaObject>( "getInstance");
	}
	
	public static void initTestInstance()
	{
		Debug.Log("Test Instance on");
		if( Application.platform != RuntimePlatform.Android )
			return;

		int debugMode = 2;

		using( var pluginClass = new AndroidJavaClass( SUPERSONIC_PLUGIN_PACKAGE ) )
			_plugin = pluginClass.CallStatic<AndroidJavaObject>( "getInstance", debugMode);

	}

	public static void release(){

		if( Application.platform != RuntimePlatform.Android )
			return;

		if (_plugin == null)
			return;

		_plugin.Call("release");

	}

	public static void onResume(){
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		if (_plugin == null)
			return;
		_plugin.Call("onResume");
	}

	public static void onPause(){
		if( Application.platform != RuntimePlatform.Android )
			return;
		
		if (_plugin == null)
			return;
		_plugin.Call("onPause");
	}

	/*Advertiser*/
	public static void reportAppStarted()
	{
		if( Application.platform != RuntimePlatform.Android )
			return;

		AndroidJavaObject jClass = new AndroidJavaObject ( SUPERSONIC_PLUGIN_PACKAGE );
		jClass.CallStatic( "reportAppStarted");
	}

	/*Offerwall*/
	public static void showOfferWall( string applicationKey, string applicationUserId)
	{
		showOfferWall (applicationKey,applicationUserId,null);
	}
	public static void showOfferWall( string applicationKey, string applicationUserId, Dictionary<string,object> additionalParameters )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		if (_plugin == null)
			return;
		var json = additionalParameters != null ? MiniJSON.Json.Serialize( additionalParameters ) : string.Empty;
		_plugin.Call( "showOfferWall", applicationKey ?? string.Empty, applicationUserId ?? string.Empty, json ?? string.Empty );
	}
	public static void getOfferWallCredits( string applicationKey, string applicationUserId )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		if (_plugin == null)
			return;
		_plugin.Call( "getOfferWallCredits", applicationKey ?? string.Empty, applicationUserId ?? string.Empty);
	}

	/*RewardedVideo*/
	public static void initRewardedVideo( string applicationKey, string applicationUserId)
	{
		initRewardedVideo (applicationKey, applicationUserId, null);
	}
	public static void initRewardedVideo( string applicationKey, string applicationUserId, Dictionary<string,object> additionalParameters )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		if (_plugin == null)
			return;
		var json = additionalParameters != null ? MiniJSON.Json.Serialize( additionalParameters ) : string.Empty;
		_plugin.Call( "initRewardedVideo", applicationKey ?? string.Empty, applicationUserId ?? string.Empty, json ?? string.Empty );
	}
	public static void showRewardedVideo()
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		if (_plugin == null)
			return;
		_plugin.Call( "showRewardedVideo");
	}

	/*Interstitial*/
	public static void initInterstitial( string applicationKey, string applicationUserId )
	{
		initInterstitial (applicationKey, applicationUserId, null);
	}
	public static void initInterstitial( string applicationKey, string applicationUserId, Dictionary<string,object> additionalParameters )
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		if (_plugin == null)
			return;
		var json = additionalParameters != null ? MiniJSON.Json.Serialize( additionalParameters ) : string.Empty;
		_plugin.Call( "initInterstitial", applicationKey ?? string.Empty, applicationUserId ?? string.Empty, json ?? string.Empty );
	}

	public static bool isInterstitialAdAvailable(){
		if( Application.platform != RuntimePlatform.Android )
			return false;
		if (_plugin == null)
			return false;
		return _plugin.Call<bool> ("isInterstitialAdAvailable");
	}

	public static void showInterstitial()
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		if (_plugin == null)
			return;
		_plugin.Call( "showInterstitial");
	}
	public static void forceShowInterstitial()
	{
		if( Application.platform != RuntimePlatform.Android )
			return;
		if (_plugin == null)
			return;
		_plugin.Call( "forceShowInterstitial");
	}


#endif
}

