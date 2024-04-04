//
//  VideoRecorderUIOverlayWindow.m
//  Unity-iPhone
//
//  Created by Ayyappa J on 23/04/20.
//

#import "VideoRecorderUIOverlayWindow.h"

@implementation VideoRecorderUIOverlayWindow

- (nullable UIView *)hitTest:(CGPoint)point withEvent:(nullable UIEvent *)event
{
    UIView *view = [super hitTest:point withEvent:event];
    
    if(view == self)
        return nil;
    else
        return view;
}

@end
