//
//  SupersonicManager.h
//  Supersonic
//
//  Created by SSA on 5/21/13.
//  Copyright (c) 2013 SSA. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SupersonicAdsPublisher.h"
#import "SupersonicAdsAdvertiser.h"


@interface SupersonicManager : NSObject<SSARewardedVideoDelegate,SSAOfferWallDelegate,SSAInterstitialDelegate>

+ (SupersonicManager*)sharedManager;


+(void)reportAppStarted;

-(void)showInterstitial;
-(void)forceShowInterstitial;

-(void)initInterstitialWithApplicationKey:(NSString *)applicationKey userId:(NSString *)userId extraParameters:(NSDictionary *)parameters;


-(void)initRewardedVideoWithApplicationKey:(NSString *)applicationKey userId:(NSString *)userId extraParameters:(NSDictionary *)parameters;

-(void)showRewardedVideo;

-(void)showOfferWallWithApplicationKey:(NSString*)applicationKey userId:(NSString*)userId extraParameters:(NSDictionary*)parameters;


-(void)getOfferWallCreditsWithApplicationKey:(NSString*)applicationKey userId:(NSString*)userId;


@end
