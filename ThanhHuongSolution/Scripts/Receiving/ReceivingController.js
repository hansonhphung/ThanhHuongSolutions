var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.select']);
app.controller('ReceivingController', function ($scope, toastr, $http) {

    $scope.shoppingCart = [];
    $scope.pagingSource = [];
    $scope.needUpdateProduct = [];

    $scope.maxSize = 1;
    $scope.recordPerPage = 1;
    $scope.pageIndex = 1;
    $scope.totalAmount = 0;
    $scope.incurredCost = 0; //chi phi phat sinh

    $scope.init = function (data) {
        $scope.lstProduct = data.LstProduct;
        $scope.number = 0;
        $scope.inputPrice = 0;
        $scope.createBillDate = data.CreateBillDate;
    }

    $scope.selectProduct = function (selectedProduct) {
        $scope.selectedProduct = selectedProduct;
        $scope.inputPrice = 0;
        $scope.number = 0;
    }
});