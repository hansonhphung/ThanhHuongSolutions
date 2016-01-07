var app = angular.module('ThanhHuongSolution', ['toastr']);
app.controller('ProductController', function ($scope, toastr, $http) {
    $scope.init = function (data) {
        $scope.availableProduct = data.LstProduct;
        $scope.products = data.LstProduct;
    }
});