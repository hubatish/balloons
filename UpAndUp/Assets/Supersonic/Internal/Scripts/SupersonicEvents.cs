using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class SupersonicEvents : MonoBehaviour
{	
	//Change the name of the attached gameObject to SupersonicEvents just incase its not
	void Awake()
	{
		gameObject.name = "SupersonicEvents";
		DontDestroyOnLoad( gameObject );
	}

	/*RewardedVideo Events*/
	private static event Action<Dictionary<string,object>> _onRVInitSuccessEvent;
	public static event Action<Dictionary<string,object>> onRVInitSuccessEvent{
		add
		{
			if (_onRVInitSuccessEvent == null || !_onRVInitSuccessEvent.GetInvocationList().Contains(value))
			{
				_onRVInitSuccessEvent += value;
			}
		}
		remove
		{
			_onRVInitSuccessEvent -= value;
		}
	}
	
	private static event Action<String> _onRVInitFailEvent;
	public static event Action<String> onRVInitFailEvent{
		add
		{
			if (_onRVInitFailEvent == null || !_onRVInitFailEvent.GetInvocationList().Contains(value))
			{
				_onRVInitFailEvent += value;
			}
		}
		remove
		{
			_onRVInitFailEvent -= value;
		}
	}
	
	private static event Action _onRVNoMoreOffersEvent;
	public static event Action onRVNoMoreOffersEvent{
		add
		{
			if (_onRVNoMoreOffersEvent == null || !_onRVNoMoreOffersEvent.GetInvocationList().Contains(value))
			{
				_onRVNoMoreOffersEvent += value;
			}
		}
		remove
		{
			_onRVNoMoreOffersEvent -= value;
		}
	}
	
	private static event Action<String> _onRVShowFailEvent;
	public static event Action<String> onRVShowFailEvent{
		add
		{
			if (_onRVShowFailEvent == null || !_onRVShowFailEvent.GetInvocationList().Contains(value))
			{
				_onRVShowFailEvent += value;
			}
		}
		remove
		{
			_onRVShowFailEvent -= value;
		}
	}
	
	private static event Action _onRVWillOpenEvent;
	public static event Action onRVWillOpenEvent{
		add
		{
			if (_onRVWillOpenEvent == null || !_onRVWillOpenEvent.GetInvocationList().Contains(value))
			{
				_onRVWillOpenEvent += value;
			}
		}
		remove
		{
			_onRVWillOpenEvent -= value;
		}
	}
	
	private static event Action _onRVDidCloseEvent;
	public static event Action onRVDidCloseEvent{
		add
		{
			if (_onRVDidCloseEvent == null || !_onRVDidCloseEvent.GetInvocationList().Contains(value))
			{
				_onRVDidCloseEvent += value;
			}
		}
		remove
		{
			_onRVDidCloseEvent -= value;
		}
	}
	
	private static event Action<int> _onRVAdCreditedEvent;
	public static event Action<int> onRVAdCreditedEvent{
		add
		{
			if (_onRVAdCreditedEvent == null || !_onRVAdCreditedEvent.GetInvocationList().Contains(value))
			{
				_onRVAdCreditedEvent += value;
			}
		}
		remove
		{
			_onRVAdCreditedEvent -= value;
		}
	}
	
	
	
	/*Interstitial Events*/
	private static event Action _onISInitSuccessEvent;
	public static event Action onISInitSuccessEvent{
		add
		{
			if (_onISInitSuccessEvent == null || !_onISInitSuccessEvent.GetInvocationList().Contains(value))
			{
				_onISInitSuccessEvent += value;
			}
		}
		remove
		{
			_onISInitSuccessEvent -= value;
		}
	}
	
	
	private static event Action<String> _onISInitFailEvent;
	public static event Action<String> onISInitFailEvent{
		add
		{
			if (_onISInitFailEvent == null || !_onISInitFailEvent.GetInvocationList().Contains(value))
			{
				_onISInitFailEvent += value;
			}
		}
		remove
		{
			_onISInitFailEvent -= value;
		}
	}
	

	private static event Action _onISShowSuccessEvent;
	public static event Action onISShowSuccessEvent{
		add {
			if (_onISShowSuccessEvent == null || !_onISShowSuccessEvent.GetInvocationList ().Contains (value)) 
			{
				_onISShowSuccessEvent += value;
			}
		}
		remove {
			_onISShowSuccessEvent -= value;
		}
	}
	
	private static event Action _onISDidCloseEvent;
	public static event Action onISDidCloseEvent{
		add {
			if (_onISDidCloseEvent == null || !_onISDidCloseEvent.GetInvocationList ().Contains (value)) 
			{
				_onISDidCloseEvent += value;
			}
		}
		remove {
			_onISDidCloseEvent -= value;
		}
	}

	private static event Action _onISAdClickedEvent;
	public static event Action onISAdClickedEvent{
		add {
			if (_onISAdClickedEvent == null || !_onISAdClickedEvent.GetInvocationList ().Contains (value)) 
			{
				_onISAdClickedEvent += value;
			}
		}
		remove {
			_onISAdClickedEvent -= value;
		}
	}
	
	private static event Action<String> _onISShowFailEvent;
	public static event Action<String> onISShowFailEvent{
		add {
			if (_onISShowFailEvent == null || !_onISShowFailEvent.GetInvocationList ().Contains (value)) 
			{
				_onISShowFailEvent += value;
			}
		}
		remove {
			_onISShowFailEvent -= value;
		}
	}
	private static event Action<bool> _onISAvailabilityEvent;
	public static event Action<bool> onISAvailabilityEvent{
		add {
			if (_onISAvailabilityEvent == null || !_onISAvailabilityEvent.GetInvocationList ().Contains (value)) 
			{
				_onISAvailabilityEvent += value;
			}
		}
		remove {
			_onISAvailabilityEvent -= value;
		}
	}
	
	/*Generic Events*/
	private static event Action _onGFSuccessEvent;
	public static event Action onGFSuccessEvent{
		add {
			if (_onGFSuccessEvent == null || !_onGFSuccessEvent.GetInvocationList ().Contains (value)) 
			{
				_onGFSuccessEvent += value;
			}
		}
		remove {
			_onGFSuccessEvent -= value;
		}
	}
	
	private static event Action<String> _onGFFailEvent;
	public static event Action<String> onGFFailEvent{
		add {
			if (_onGFFailEvent == null || !_onGFFailEvent.GetInvocationList ().Contains (value)) 
			{
				_onGFFailEvent += value;
			}
		}
		remove {
			_onGFFailEvent -= value;
		}
	}
	
	private static event Action<Dictionary<string,object>> _onRVGenericEvent;
	public static event Action<Dictionary<string,object>> onRVGenericEvent{
		add {
			if (_onRVGenericEvent == null || !_onRVGenericEvent.GetInvocationList ().Contains (value)) 
			{
				_onRVGenericEvent += value;
			}
		}
		remove {
			_onRVGenericEvent -= value;
		}
	}
	
	private static event Action<Dictionary<string,object>> _onISGenericEvent;
	public static event Action<Dictionary<string,object>> onISGenericEvent{
		add {
			if (_onISGenericEvent == null || !_onISGenericEvent.GetInvocationList ().Contains (value)) 
			{
				_onISGenericEvent += value;
			}
		}
		remove {
			_onISGenericEvent -= value;
		}
	}
	
	private static event Action<Dictionary<string,object>> _onOWGenericEvent;
	public static event Action<Dictionary<string,object>> onOWGenericEvent{
		add {
			if (_onOWGenericEvent == null || !_onOWGenericEvent.GetInvocationList ().Contains (value)) 
			{
				_onOWGenericEvent += value;
			}
		}
		remove {
			_onOWGenericEvent -= value;
		}
	}
	
	/*Offerwall Events*/
	private static event Action _onOWShowSuccessEvent;
	public static event Action onOWShowSuccessEvent{
		add {
			if (_onOWShowSuccessEvent == null || !_onOWShowSuccessEvent.GetInvocationList ().Contains (value)) 
			{
				_onOWShowSuccessEvent += value;
			}
		}
		remove {
			_onOWShowSuccessEvent -= value;
		}
	}
	
	private static event Action<String> _onOWShowFailEvent;
	public static event Action<String> onOWShowFailEvent{
		add {
			if (_onOWShowFailEvent == null || !_onOWShowFailEvent.GetInvocationList ().Contains (value)) 
			{
				_onOWShowFailEvent += value;
			}
		}
		remove {
			_onOWShowFailEvent -= value;
		}
	}
	
	private static event Action<Dictionary<string,object>> _onOWAdCreditedEvent;
	public static event Action<Dictionary<string,object>> onOWAdCreditedEvent{
		add {
			if (_onOWAdCreditedEvent == null || !_onOWAdCreditedEvent.GetInvocationList ().Contains (value)) 
			{
				_onOWAdCreditedEvent += value;
			}
		}
		remove {
			_onOWAdCreditedEvent -= value;
		}
	}
	
	private static event Action<String> _onGetOWCreditsFailedEvent;
	public static event Action<String> onGetOWCreditsFailedEvent{
		add {
			if (_onGetOWCreditsFailedEvent == null || !_onGetOWCreditsFailedEvent.GetInvocationList ().Contains (value)) 
			{
				_onGetOWCreditsFailedEvent += value;
			}
		}
		remove {
			_onGetOWCreditsFailedEvent -= value;
		}
	}
	
	private static event Action _onOWDidCloseEvent;
	public static event Action onOWDidCloseEvent{
		add {
			if (_onOWDidCloseEvent == null || !_onOWDidCloseEvent.GetInvocationList ().Contains (value)) 
			{
				_onOWDidCloseEvent += value;
			}
		}
		remove {
			_onOWDidCloseEvent -= value;
		}
	}
	
	/*Interstital Events*/
	public void onISInitSuccess( string empty )
	{
		if( _onISInitSuccessEvent != null )
			_onISInitSuccessEvent();
	}
	public void onISInitFail( string val )
	{
		if( _onISInitFailEvent != null )
			_onISInitFailEvent( val );
	}

	public void onISShowFail( string val )
	{
		if( _onISShowFailEvent != null )
			_onISShowFailEvent( val );
		
	}
	public void onISDidClose( string empty )
	{
		if( _onISDidCloseEvent != null )
			_onISDidCloseEvent();
	}
	public void onISShowSuccess( string empty )
	{
		if( _onISShowSuccessEvent != null )
			_onISShowSuccessEvent();
	}

	public void onISAdClicked( string empty )
	{
		if( _onISAdClickedEvent != null )
			_onISAdClickedEvent();
	}
	
	public void onISAvailability( string available )
	{
		if( _onISAvailabilityEvent != null )
			_onISAvailabilityEvent(Boolean.Parse(available));
	}

	/*Offerwall Events*/
	public void onOWShowSuccess( string empty )
	{
		if( _onOWShowSuccessEvent != null )
			_onOWShowSuccessEvent();
	}
	public void onOWShowFail( string val )
	{
		if( _onOWShowFailEvent != null )
			_onOWShowFailEvent( val );
	}
	public void onOWDidClose( string empty )
	{
		if( _onOWDidCloseEvent != null )
			_onOWDidCloseEvent();
	}
	public void onOWAdCredited( string json )
	{
		if( _onOWAdCreditedEvent != null )
			_onOWAdCreditedEvent( MiniJSON.Json.Deserialize( json ) as Dictionary<string,object> );
	}
	public void onGetOWCreditsFailed( string val )
	{
		if( _onGetOWCreditsFailedEvent != null )
			_onGetOWCreditsFailedEvent( val );
	}
	
	
	/*Generic Events*/
	public void onGFSuccess( string empty )
	{
		if( _onGFSuccessEvent != null )
			_onGFSuccessEvent();
	}
	public void onRVGeneric( string json )
	{
		if( _onRVGenericEvent != null )
			_onRVGenericEvent( MiniJSON.Json.Deserialize( json ) as Dictionary<string,object> );
	}
	public void onISGeneric( string json )
	{
		if( _onISGenericEvent != null )
			_onISGenericEvent( MiniJSON.Json.Deserialize( json ) as Dictionary<string,object> );
	}
	public void onGFFail( string val )
	{
		if( _onGFFailEvent != null )
			_onGFFailEvent( val );
	}
	public void onOWGeneric( string json )
	{
		if( _onOWGenericEvent != null )
			_onOWGenericEvent( MiniJSON.Json.Deserialize( json ) as Dictionary<string,object> );
	}
	
	/*RewardedVideo Events*/
	public void onRVInitSuccess( string json )
	{
		if( _onRVInitSuccessEvent != null )
			_onRVInitSuccessEvent( MiniJSON.Json.Deserialize( json ) as Dictionary<string,object> );
	}
	public void onRVInitFail( string val )
	{
		if( _onRVInitFailEvent != null )
			_onRVInitFailEvent( val );
	}
	public void onRVNoMoreOffers( string empty )
	{
		if( _onRVNoMoreOffersEvent != null )
			_onRVNoMoreOffersEvent();
	}
	public void onRVShowFail( string val )
	{
		if( _onRVShowFailEvent != null )
			_onRVShowFailEvent( val );
	}
	public void onRVWillOpen( string empty )
	{
		if( _onRVWillOpenEvent != null )
			_onRVWillOpenEvent();
	}
	public void onRVDidClose( string empty )
	{
		if( _onRVDidCloseEvent != null )
			_onRVDidCloseEvent();
	}
	public void onRVAdCredited( string val )
	{
		if( _onRVAdCreditedEvent != null )
			_onRVAdCreditedEvent( int.Parse(val) );
	}
	
	
	
}

