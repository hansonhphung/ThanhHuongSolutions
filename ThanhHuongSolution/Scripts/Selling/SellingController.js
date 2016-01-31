var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.select']);
app.controller('SellingController', function ($scope, toastr, $http) {

    $scope.shoppingCart = [];

    $scope.init = function (data)
    {
        $scope.lstCustomer = data.LstCustomer;
        $scope.lstProduct = data.LstProduct;
        $scope.number = 0;
        $scope.createBillDate = data.CreateBillDate;
    }

    $scope.selectCustomer = function (selectedCustomer)
    {
        $scope.selectedCustomer = selectedCustomer;
    }

    $scope.selectProduct = function (selectedProduct)
    {
        $scope.selectedProduct = selectedProduct;

        $scope.wholesalePrice = selectedProduct.WholesalePrice;
        $scope.retailPrice = selectedProduct.RetailPrice;

        $scope.number = 0;
    }

    $scope.addProductItem = function ()
    {
        if ($scope.selectedProduct == null || $scope.selectedProduct == undefined)
        {
            toastr.warning("Vui lòng chọn sản phẩm.");
            return;
        }

        if ($scope.number == null || $scope.number == undefined || $scope.number == '')
        {
            toastr.warning("Vui lòng nhập số lượng sản phẩm");
            return;
        }

        var price = $scope.wholesalePrice; // price of per item

        var isExist = false;

        for (var i = 0; i < $scope.shoppingCart.length; i++)
        {
            if ($scope.shoppingCart[i].TrackingNumber == $scope.selectedProduct.TrackingNumber) //already in cart
            {
                $scope.shoppingCart[i].Number += $scope.number;
                isExist = true;
            }
        }

        if (!isExist)
        {
            $scope.shoppingCart.push({ TrackingNumber: $scope.selectedProduct.TrackingNumber, Name: $scope.selectedProduct.Name, Number: $scope.number, TotalPrice: price * $scope.number });
        }
    }

    $scope.deleteItemInCart = function (trackingNumber)
    {
        for (var i = 0; i < $scope.shoppingCart.length; i++)
        {
            if ($scope.shoppingCart[i].TrackingNumber == trackingNumber)
            {
                $scope.shoppingCart.splice(i, 1);
            }
        }
    }

    $scope.initData = function ()
    {
        $scope.shoppingCart = [];

        $scope.selectedProduct = $scope.lstProduct[0];

        $scope.selecteCustomer = $scope.lstCustomer[0];

        $scope.wholesalePrice = $scope.selectedProduct.WholesalePrice;

        $scope.retailPrice = $scope.selectedProduct.RetailPrice;

        $scope.number = 0;
    }

    $scope.cancel = function ()
    {
        $scope.initData();
    }
});