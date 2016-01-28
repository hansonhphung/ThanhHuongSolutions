var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.select']);
app.controller('SellingController', function ($scope, toastr, $http) {

    $scope.init = function (data)
    {
        alert(JSON.stringify(data));
        $scope.lstCustomer = data.LstCustomer;
        $scope.lstProduct = data.LstProduct;

        $scope.selectedCustomer = $scope.lstCustomer[0];
        $scope.selectedProduct = $scope.lstProduct[0];
    }
});