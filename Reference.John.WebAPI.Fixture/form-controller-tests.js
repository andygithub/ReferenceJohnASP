'use strict';

(function () {
    describe('During construction of the controller', function() {
        var scope, controller, courseRepositoryMock, courses;

        beforeEach(function() {
            module('registrationModule');

            inject(function($rootScope, $controller, courseRepository) {
                scope = $rootScope.$new();
                courseRepositoryMock = sinon.stub(courseRepository);
                courses = [{ foo: 'bar' }];
                courseRepositoryMock.get.returns(courses);
                controller = $controller('CoursesController', { $scope: scope });
            });
        });

        it('should set the courses from the repository', function() {
            expect(scope.courses).toBe(courses);
        });
    });
}());