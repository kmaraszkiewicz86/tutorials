//
//  Course.h
//  ObjCGettingStarted
//
//  Created by Krzysztof Maraszkiewicz on 30/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface Course : NSObject

@property (strong, nonatomic) NSString *name;
@property (strong, nonatomic) NSNumber *numberOfLessons; 

@end

NS_ASSUME_NONNULL_END
