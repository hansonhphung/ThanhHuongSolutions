var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.bootstrap']);
app.controller('ProductController', function ($scope, toastr, $http) {

    $scope.productTypes = [{ 'key': 'Tất cả', 'value': '0' }, { 'key': 'Lương thực', 'value': '1' }, { 'key': 'Phân bón', 'value': '2' }];
    $scope.pageSize = 2;
    $scope.pageIndex = 1;
    $scope.maxsize = 5;
    $scope.pagingSource = [];

    $scope.init = function (data) {
        $scope.availableProduct = data.LstProduct;
        $scope.pagingSource = $scope.availableProduct;
        $scope.selectedProductType = $scope.productTypes[0];
        
        $scope.updatePagingConfig();

        $scope.onChangePageIndex();
    }

    $scope.updatePagingConfig = function()
    {
        $scope.totalServerItems = $scope.pagingSource.length;

        $scope.numPages = Math.ceil($scope.totalServerItems / $scope.pageSize);
    }

    $scope.search = function () {

        $http.post("/Product/Search", {query: $scope.query}, {
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
    
    $scope.getProductType = function (type)
    {
        switch (type)
        {
            case 1: return 'Lương thực';
            case 2: return 'Phân bón';
        }
        return '';
    }

    $scope.getProductType = function (type) {
        switch (type) {
            case 1: return 'Lương thực';
            case 2: return 'Phân bón';
        }
        return '';
    }

    $scope.getUnitType = function (type) {
        switch (type) {
            case 1: return 'Kg';
        }
        return '';
    }

    $scope.onChangeProductType = function (productType)
    {
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

    $scope.onChangePageIndex = function ()
    {
        $scope.products = [];
        if ($scope.pageIndex == $scope.numPages) {
            for (var i = ($scope.pageIndex - 1) * $scope.pageSize; i < $scope.pagingSource.length; i++) {
                $scope.products.push($scope.pagingSource[i]);
            }
        }
        else {
            for (var i = 0; i < $scope.pageSize; i++)
            {
                var index = ($scope.pageIndex - 1) * $scope.pageSize + i;
                $scope.products.push($scope.pagingSource[index]);
            }
        }
    }
});