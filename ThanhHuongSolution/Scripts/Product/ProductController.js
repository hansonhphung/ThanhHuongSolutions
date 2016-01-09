var app = angular.module('ThanhHuongSolution', ['toastr']);
app.controller('ProductController', function ($scope, toastr, $http) {

    $scope.productTypes = [{ 'key': 'Tất cả', 'value': '0' }, { 'key': 'Lương thực', 'value': '1' }, {'key':'Phân bón', 'value':'2'}];

    $scope.init = function (data) {
        $scope.availableProduct = data.LstProduct;
        $scope.products = data.LstProduct;
        $scope.selectedProductType = $scope.productTypes[0];
    }

    $scope.search = function () {
        //$scope.query = '';

        $http.post("/Product/Search", {query: $scope.query}, {
        }).success(function (response) {
            if (response.isSuccess) {
                var data = response.data;

                $scope.availableProduct = data;

                $scope.products = data;
            }
            else {
                alert('error at: ' + response.message);
            }
        });
    }

    $scope.searchKeyDown = function (event) {
        if (event.keyCode == 13) {
            $scope.search();
            //$scope.searchProduct();
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
        $scope.products = [];

        if (productType.value == 0)
            $scope.products = $scope.availableProduct;
        else {
            for (var i = 0; i < $scope.availableProduct.length; i++) {
                if ($scope.availableProduct[i].ProductType == productType.value)
                    $scope.products.push($scope.availableProduct[i]);
            }
        }
    }
});