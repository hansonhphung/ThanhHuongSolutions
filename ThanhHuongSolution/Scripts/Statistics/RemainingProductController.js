var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.bootstrap']);
app.controller('RemainingProductController', function ($scope, toastr, $http) {

    $scope.productTypes = [{ 'key': 'Tất cả', 'value': '0' }, { 'key': 'Phân bón', 'value': '1' }, { 'key': 'Gạo', 'value': '2' }, { 'key': 'Cà phê', 'value': '3' }];
    $scope.newProductTypes = [{ 'key': 'Phân bón', 'value': '1' }, { 'key': 'Gạo', 'value': '2' }, { 'key': 'Cà phê', 'value': '3' }];
    $scope.pageSize = 10;
    $scope.pageIndex = 1;
    $scope.maxsize = 5;
    $scope.pagingSource = [];
    $scope.query = '';
    $scope.totalRemainingProductCost = 0;

    $scope.init = function () {
        $scope.selectedProductType = $scope.productTypes[0];
        $scope.newSelectedProductType = $scope.newProductTypes[0];

        $scope.search();
    }

    $scope.updatePagingConfig = function () {
        $scope.totalServerItems = $scope.pagingSource.length;

        $scope.numPages = Math.ceil($scope.totalServerItems / $scope.pageSize);
    }

    $scope.search = function () {

        $http.post("/Statistics/SearchRemainingProduct", { query: $scope.query }, {
        }).success(function (response) {
            if (response.isSuccess) {
                var data = response.data;

                $scope.availableProduct = data;

                $scope.pagingSource = data;

                $scope.updatePagingConfig();

                $scope.pageIndex = 1;

                $scope.onChangePageIndex();
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
    }

    $scope.searchKeyDown = function (event) {
        if (event.keyCode == 13) {
            $scope.search();
            $scope.selectedProductType = $scope.productTypes[0];
        }
    }

    $scope.getProductType = function (type) {
        return $scope.newProductTypes[type - 1];
    }

    $scope.onChangeProductType = function (productType) {
        $scope.pagingSource = [];

        if (productType.value == 0)
            $scope.pagingSource = $scope.availableProduct;
        else {
            for (var i = 0; i < $scope.availableProduct.length; i++) {
                if ($scope.availableProduct[i].ProductType == productType.value)
                    $scope.pagingSource.push($scope.availableProduct[i]);
            }
        }

        $scope.pageIndex = 1;

        $scope.updatePagingConfig();

        $scope.onChangePageIndex();
    }

    $scope.onChangePageIndex = function () {
        $scope.totalRemainingProductCost = 0;
        $scope.products = [];
        if ($scope.numPages == 0)
            return;

        if ($scope.pageIndex == $scope.numPages) {
            for (var i = ($scope.pageIndex - 1) * $scope.pageSize; i < $scope.pagingSource.length; i++) {
                $scope.products.push($scope.pagingSource[i]);
            }
        }
        else {
            for (var i = 0; i < $scope.pageSize; i++) {
                var index = ($scope.pageIndex - 1) * $scope.pageSize + i;
                $scope.products.push($scope.pagingSource[index]);
            }
        }

        for (var i = 0; i < $scope.products.length; i++) {
            $scope.totalRemainingProductCost += $scope.products[i].TotalCost;
        }
    }
});