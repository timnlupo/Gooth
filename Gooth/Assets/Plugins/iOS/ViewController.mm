//
//  ViewController.mm
//  
//
//  Created by Tim Lupo on 6/20/15.
//
//

#import "ViewController.h"

@implementation ViewController: UIViewController

    //Method for only text sharing
    -(void) shareOnlyTextMethod: (const char *) shareMessage: (const char *) shareLink
    {
        NSString *shareText = [NSString stringWithUTF8String:shareMessage];
        NSURL *shareURL = [NSURL URLWithString:[NSString stringWithUTF8String:shareLink]];
        NSArray *itemsToShare = @[shareText, shareURL];
        UIActivityViewController *activityVC = [[UIActivityViewController alloc] initWithActivityItems:itemsToShare applicationActivities:nil];
        if ( UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad ) {
            UIPopoverController *popup = [[UIPopoverController alloc] initWithContentViewController:activityVC];
            [popup presentPopoverFromRect:CGRectMake(self.view.frame.size.width/2, self.view.frame.size.height/4, 0, 0)inView:[UIApplication sharedApplication].keyWindow.rootViewController.view permittedArrowDirections:UIPopoverArrowDirectionAny animated:YES];
        }
        else {
            [self presentViewController:activityVC animated:YES completion:nil];
            [[UIApplication sharedApplication].keyWindow.rootViewController presentViewController:activityVC animated:YES completion:nil];
        }
    }

@end
 
// Globally declare text sharing method
extern "C"{
    void sampleTextMethod(const char *message, const char *link){
        ViewController *vc = [[ViewController alloc] init];
        [vc shareOnlyTextMethod: message: link];
    }
}