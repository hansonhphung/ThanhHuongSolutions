var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.select']);
app.controller('SellingController', function ($scope, toastr, $http) {

    $scope.init = function (data)
    {
        alert(JSON.stringify(data));
        $scope.lstCustomer = data.LstCustomer;
        $scope.lstProduct = data.LstProduct;
    }

    $scope.selecteCustomer = function (selectedCustomer)
    {
        $scope.selectedCustomer = selectedCustomer;
    }

    $scope.selectProduct = function (selectedProduct)
    {
        $scope.selectedProduct = selectedProduct;

        $scope.wholesalePrice = selectedProduct.WholesalePrice;
        $scope.retailPrice = selectedProduct.RetailPrice;
    }
});