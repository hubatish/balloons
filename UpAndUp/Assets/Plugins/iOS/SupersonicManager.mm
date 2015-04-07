//
//  SupersonicManager.m
//  Supersonic
//
//  Created by SSA on 5/21/13.
//  Copyright (c) 2013 SSA. All rights reserved.
//

#import "SupersonicManager.h"
#import "JSONKit.h"


void UnityPause( bool shouldPause );
void UnitySendMessage( const char * className, const char * methodName, const char * param );


@implementation SupersonicManager
SupersonicAdsPublisher *ssaAg;
///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - NSObject

+ (SupersonicManager*)sharedManager
{
    return [SupersonicManager sharedManager:false];
}
+ (SupersonicManager*)sharedManager:(BOOL)isTestInstance
{
    static SupersonicManager *instance;
    static dispatch_once_t onceToken;
    dispatch_once( &onceToken,
                  ^{
                      instance = [[SupersonicManager alloc] init];
                      if(isTestInstance){
                          NSLog(@"Test Instance On");
                          ssaAg = [SupersonicAdsPublisher sharedTestInstance];
                      }
                      else
                          ssaAg = [SupersonicAdsPublisher sharedInstance];
                  });
    
    return instance;
}
-(NSString *)getJsonFromDic:(NSDictionary *)dict{
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict
                                                       options:0
                                                         error:&error];
    if (! jsonData) {
        NSLog(@"Got an error: %@", error);
        return @"";
    } else {
        NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        return jsonString;
    }
    
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// API used by the C Side

////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - Public

///// Advertiser /////
+(void)reportAppStarted{
    [[SupersonicAdsAdvertiser sharedInstance] reportAppStarted];
}

///// Interstitial /////
-(void)initInterstitialWithApplicationKey:(NSString *)applicationKey userId:(NSString *)userId extraParameters:(NSDictionary *)parameters{
    NSMutableDictionary* temp = [[NSMutableDictionary alloc] initWithDictionary:parameters];
    [ssaAg initInterstitialWithApplicationKey:applicationKey userId:userId delegate:self additionalParameters:temp];
}
-(bool)isInterstitialAdAvailable{
    return [ssaAg isInterstitialAdAvailable];
}
-(void)showInterstitial{
    [ssaAg showInterstitial];
}
-(void)forceShowInterstitial{
    [ssaAg forceShowInterstitial];
}

///// RewardedVideo /////
-(void)initRewardedVideoWithApplicationKey:(NSString *)applicationKey userId:(NSString *)userId extraParameters:(NSDictionary *)parameters{
    NSMutableDictionary* temp = [[NSMutableDictionary alloc] initWithDictionary:parameters];
    [ssaAg initRewardedVideoWithApplicationKey:applicationKey userId:userId delegate:self additionalParameters:temp];
}
-(void)showRewardedVideo{
    [ssaAg showRewardedVideo];
}

///// OfferWall /////
- (void)showOfferWallWithApplicationKey:(NSString*)applicationKey userId:(NSString*)userId extraParameters:(NSDictionary*)parameters
{
    NSMutableDictionary* temp = [[NSMutableDictionary alloc] initWithDictionary:parameters];
    [ssaAg showOfferWallWithApplicationKey:applicationKey userId:userId delegate:self additionalParameters:temp];
}
-(void)getOfferWallCreditsWithApplicationKey:(NSString *)applicationKey userId:(NSString *)userId{
    [ssaAg getOfferWallCreditsWithApplicationKey:applicationKey userId:userId delegate:self];
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//                                              Delegates

////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - OfferWallDelegate

- (void)ssaOfferWallShowSuccess
{
    
    UnitySendMessage( "SupersonicEvents", "onOWShowSuccess", "" );
    
}
- (void)ssaOfferWallShowFailedWithError:(NSError *)errorInfo
{
    
    UnitySendMessage( "SupersonicEvents", "onOWShowFail", errorInfo.localizedDescription.UTF8String );
    
}
- (BOOL)ssaOfferWallDidReceiveCredit:(NSDictionary *)creditInfo
{
    
    UnitySendMessage( "SupersonicEvents", "onOWAdCredited", [self getJsonFromDic:creditInfo].UTF8String );
    return true;
    
}
- (void)ssaOfferwallDidFailGettingCreditWithError:(NSError *)error
{
    
    UnitySendMessage( "SupersonicEvents", "onGetOWCreditsFailed", error.localizedDescription.UTF8String );
    
}
- (void)ssaOfferWallDidClose
{
    
    UnitySendMessage( "SupersonicEvents", "onOWDidClose", "" );
    
}

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - Rewarded Video Delegate

-(void)ssaRewardedVideoDidUpdateAdUnits:(NSDictionary *) adUnitsInfo{
    
    UnitySendMessage( "SupersonicEvents", "onRVInitSuccess", [self getJsonFromDic:adUnitsInfo].UTF8String );
    
}

-(void)ssaRewardedVideoDidFailInitWithError:(NSError *)errorInfo
{
    
    UnitySendMessage( "SupersonicEvents", "onRVInitFail", errorInfo.localizedDescription.UTF8String );
    
}

-(void)ssaRewardedVideoDidReceiveCredit:(NSDictionary  *)creditInfo{
    
    NSString *str = [NSString stringWithFormat:@"%@",creditInfo[@"credits"]];
    UnitySendMessage( "SupersonicEvents", "onRVAdCredited", [str UTF8String] );
    
    
}

-(void)ssaRewardedVideoNoMoreOffers{
    
    UnitySendMessage( "SupersonicEvents", "onRVNoMoreOffers", "");
    
}

-(void)ssaRewardedVideoDidFailShowWithError:(NSError *)error{
    
    UnitySendMessage( "SupersonicEvents", "onRVShowFail", error.localizedDescription.UTF8String );
    
}

-(void)ssaRewardedVideoWindowDidClose{
    
    UnitySendMessage( "SupersonicEvents", "onRVDidClose","");
    
}

-(void)ssaRewardedVideoWindowWillOpen{
    
    UnitySendMessage( "SupersonicEvents", "onRVWillOpen", "" );
    
}
///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - Interstitial Delegate

-(void) ssaInitInterstitialSuccess{
    
    UnitySendMessage( "SupersonicEvents", "onISInitSuccess", "" );
    
}

-(void)ssaInitInterstitialFailWithError:(NSError *)error{
    
    UnitySendMessage( "SupersonicEvents", "onISInitFail", error.localizedDescription.UTF8String );
    
}

-(void)ssaInterstitialAdAvailable:(BOOL)available{
    
    UnitySendMessage( "SupersonicEvents", "onISAvailability", (available) ? "true" : "false" );
    
}

-(void) ssaShowInterstitialSuccess{
    
    UnitySendMessage( "SupersonicEvents", "onISShowSuccess", "" );
    
}

-(void)ssaInterstitialAdClosed{
    
    UnitySendMessage( "SupersonicEvents", "onISDidClose", "" );
    
}

-(void)ssaInterstitialAdClicked{
    
    UnitySendMessage( "SupersonicEvents", "onISAdClicked", "" );
    
}

-(void)ssaShowInterstitialFailWithError:(NSError *)error{
    
    UnitySendMessage( "SupersonicEvents", "onISShowFail", error.localizedDescription.UTF8String );
    
}

@end


////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// C Side of the bridge, the C# Script in Unity will directly call the C Side, which will then call the ObjC Side

////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#ifdef __cplusplus
extern "C" {
#endif
    
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]
#define GetStringParamOrNil( _x_ ) ( _x_ != NULL && strlen( _x_ ) ) ? [NSString stringWithUTF8String:_x_] : nil
    
    /// Advertiser ///
    void _supersonicReportAppStarted(){
        [SupersonicManager reportAppStarted];
    }
    
    /// Publisher ///
    void _supersonicInit(){
        [SupersonicManager sharedManager];
    }
    void _supersonicInitTestInstance(){
        [SupersonicManager sharedManager:true];
    }
    
    /// Interstitial ///
    void _supersonicInitInterstitial( const char * applicationKey, const char * userId, const char * additionalParameters )
    {
        NSDictionary *params = nil;
        NSString *json = GetStringParamOrNil( additionalParameters );
        if( json )
            params = (NSDictionary*)[json objectFromJSONString];
        
        [[SupersonicManager sharedManager] initInterstitialWithApplicationKey:GetStringParam(applicationKey) userId:GetStringParamOrNil(userId) extraParameters:params];
    }
    
    bool _supersonicIsInterstitialAdAvailable(){
        return [[SupersonicManager sharedManager] isInterstitialAdAvailable];
    }
    
    void _supersonicShowInterstitial(){
        [[SupersonicManager sharedManager] showInterstitial];
    }
    void _supersonicForceShowInterstitial(){
        [[SupersonicManager sharedManager] forceShowInterstitial];
    }
    
    /// RewardedVideo ///
    void _supersonicInitRewardedVideo( const char * applicationKey, const char * userId, const char * additionalParameters )
    {
        NSDictionary *params = nil;
        NSString *json = GetStringParamOrNil( additionalParameters );
        if( json )
            params = (NSDictionary*)[json objectFromJSONString];
        
        [[SupersonicManager sharedManager] initRewardedVideoWithApplicationKey:GetStringParam(applicationKey) userId:GetStringParamOrNil(userId) extraParameters:params];
    }
    
    void _supersonicShowRewardedVideo(){
        [[SupersonicManager sharedManager] showRewardedVideo];
    }
    
    /// OfferWall ///
    void _supersonicShowOfferWall( const char * applicationKey, const char * userId, const char * additionalParameters )
    {
        NSDictionary *params = nil;
        NSString *json = GetStringParamOrNil( additionalParameters );
        if( json )
            params = (NSDictionary*)[json objectFromJSONString];
        
        [[SupersonicManager sharedManager] showOfferWallWithApplicationKey:GetStringParam( applicationKey )
                                                                    userId:GetStringParamOrNil( userId )
                                                           extraParameters:params];
    }
    void _supersonicGetOfferWallCredits(const char * applicationKey,const char *userId){
        
        [[SupersonicManager sharedManager] getOfferWallCreditsWithApplicationKey:GetStringParam(applicationKey) userId:GetStringParamOrNil(userId)];
    }
    
    
#ifdef __cplusplus
}
#endif
