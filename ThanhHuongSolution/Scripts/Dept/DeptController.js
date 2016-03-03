var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.bootstrap']);
app.controller('DeptController', function ($scope, toastr, $location, $http){
    
    //Pagination
    $scope.pageIndex = 1;
    $scope.recordPerPage = 3;
    $scope.maxSize = 5;
    $scope.pagingSource = [];
    $scope.currentIndex = 1;
    $scope.isSearchName = true
});