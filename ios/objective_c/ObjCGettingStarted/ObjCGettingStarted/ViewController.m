//
//  ViewController.m
//  ObjCGettingStarted
//
//  Created by Krzysztof Maraszkiewicz on 29/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

#import "ViewController.h"
#import "Course.h"

@interface ViewController ()

@property (strong, nonatomic) NSMutableArray<Course *> *courses;

@end

@implementation ViewController

NSString *cellId = @"cellId";

- (void)viewDidLoad {
    [super viewDidLoad];
    
    [self setupCourses];
    
    self.navigationItem.title = @"Courses";
    self.navigationController.navigationBar.prefersLargeTitles = YES;
    
    [self.tableView registerClass:UITableViewCell.class forCellReuseIdentifier:cellId];
}

- (void)setupCourses {
    self.courses = NSMutableArray.new;
        
        Course *course = Course.new;
    //    Course *course = [[Course alloc] init];
        course.name = @"Instagram Firebase";
        course.numberOfLessons = @(49);
        
        [self.courses addObject:course];
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return self.courses.count;
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:cellId forIndexPath:indexPath];
    
    Course *course = self.courses[indexPath.row];
    
    cell.textLabel.text = course.name;
    
    return cell;
}


@end
