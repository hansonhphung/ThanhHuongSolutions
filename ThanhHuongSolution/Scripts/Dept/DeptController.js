var app = angular.module('ThanhHuongSolution', ['toastr');
app.controller('DebtController', function ($scope, toastr, $http){
    
    //Pagination
    $scope.pageIndex = 1;
    $scope.recordPerPage = 3;
    $scope.maxSize = 5;
    $scope.pagingSource = [];
    $scope.currentIndex = 1;
    $scope.isSearchName = true;
    $scope.mode = 'debt';

    $scope.switchDebtMode = function ()
    {
        $scope.mode = 'debt';
    }

    $scope.switchPaidDebtMode = function () {
        $scope.mode = 'paiddebt';
    }
});