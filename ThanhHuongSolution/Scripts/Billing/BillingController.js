var app = angular.module('ThanhHuongSolution', ['toastr']);

app.controller('BillingController', function ($scope, toastr, $http) {

    $scope.init = function (data) {
        alert(JSON.stringify(data));
        $scope.lstBilling = data.LstBilling;
        $scope.bills = data.LstBilling;
    }
});