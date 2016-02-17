var app = angular.module('ThanhHuongSolution', ['toastr']);

app.controller('BillingController', function ($scope, toastr, $http) {

    $scope.init = function (data) {
        $scope.lstBilling = data.LstBilling;
        $scope.bills = data.LstBilling;
    }

    $scope.search = function () {
        $http.post("/Billing/Search", { query: $scope.query }, {
        }).success(function (response) {
            if (response.isSuccess) {
                var data = response.data;

                $scope.bills = data;
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
    }

    $scope.searchKeyDown = function (event) {
        if (event.keyCode == 13) {
            $scope.search();
        }
    }
});