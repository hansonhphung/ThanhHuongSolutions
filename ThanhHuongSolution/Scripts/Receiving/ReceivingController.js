var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.select', 'ui.bootstrap']);
app.controller('ReceivingController', function ($scope, toastr, $http) {

    $scope.shoppingCart = [];
    $scope.pagingSource = [];
    $scope.needUpdateProduct = [];

    $scope.maxSize = 1;
    $scope.recordPerPage = 1;
    $scope.pageIndex = 1;
    $scope.totalAmount = 0;
    $scope.incurredCost = 0; //chi phi phat sinh
    $scope.finalTotalAmount = 0;

    $scope.isDisablePrice = false;

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

        var isExist = false;

        for (var i = 0; i < $scope.pagingSource.length; i++)
        {
            if ($scope.pagingSource[i].TrackingNumber == $scope.selectedProduct.TrackingNumber)
            {
                $scope.inputPrice = $scope.pagingSource[i].Price;
                $scope.isDisablePrice = true;
                return;
            }
        }

        $scope.isDisablePrice = false;
    }

    $scope.addProductItem = function () {
        if ($scope.selectedProduct == null || $scope.selectedProduct == undefined) {
            toastr.warning("Vui lòng chọn sản phẩm.");
            return;
        }

        if ($scope.inputPrice == null || $scope.inputPrice == undefined || $scope.inputPrice == '') {
            toastr.warning("Vui lòng nhập giá");
            return;
        }

        if ($scope.number == null || $scope.number == undefined || $scope.number == '') {
            toastr.warning("Vui lòng nhập số lượng sản phẩm");
            return;
        }

        var isExist = false;

        for (var i = 0; i < $scope.pagingSource.length; i++) {
            if ($scope.pagingSource[i].TrackingNumber == $scope.selectedProduct.TrackingNumber) //already in cart
            {
                $scope.pagingSource[i].Number += $scope.number;
                $scope.pagingSource[i].TotalPrice += $scope.pagingSource[i].Price * $scope.number;
                isExist = true;
                break;
            }
        }

        if (!isExist) {
            $scope.pagingSource.push({ TrackingNumber: $scope.selectedProduct.TrackingNumber, Name: $scope.selectedProduct.Name, Price: $scope.inputPrice, Number: $scope.number, TotalPrice: $scope.inputPrice * $scope.number });
        }

        $scope.totalAmount += $scope.inputPrice * $scope.number;
        $scope.finalTotalAmount += $scope.inputPrice * $scope.number;
        $scope.isDisablePrice = true;

        $scope.updatePagingConfig();
        $scope.onChangePageIndex();
    }

    $scope.updatePagingConfig = function () {
        $scope.totalProduct = $scope.pagingSource.length;
        $scope.numPages = Math.ceil($scope.totalProduct / $scope.recordPerPage);
    }

    $scope.onChangePageIndex = function () {
        $scope.shoppingCart = [];

        if ($scope.pageIndex == $scope.numPages) {
            for (var i = ($scope.pageIndex - 1) * $scope.recordPerPage; i < $scope.pagingSource.length; i++) {
                $scope.shoppingCart.push($scope.pagingSource[i]);
            }
        }
        else {
            for (var i = 0; i < $scope.recordPerPage; i++) {
                var index = ($scope.pageIndex - 1) * $scope.recordPerPage + i;
                $scope.shoppingCart.push($scope.pagingSource[index]);
            }
        }
    }

    $scope.viewItemInCart = function (trackingNumber) {
        for (var i = 0; i < $scope.pagingSource.length; i++) {
            if ($scope.pagingSource[i].TrackingNumber == trackingNumber) {
                $scope.itemTrackingNumber = $scope.pagingSource[i].TrackingNumber;
                $scope.itemName = $scope.pagingSource[i].Name;
                $scope.itemQuantity = $scope.pagingSource[i].Number;
                $scope.itemTotalPrice = $scope.pagingSource[i].TotalPrice;
                $scope.itemPrice = $scope.pagingSource[i].Price;
                break;
            }
        }
    }

    $scope.deleteItemInCart = function (trackingNumber)
    {
        for (var i = 0; i < $scope.pagingSource.length; i++) {
            if ($scope.pagingSource[i].TrackingNumber == trackingNumber) {
                $scope.totalAmount -= $scope.pagingSource[i].TotalPrice;
                $scope.finalTotalAmount -= $scope.pagingSource[i].TotalPrice;
                $scope.pagingSource.splice(i, 1);
            }
        }

        $scope.updatePagingConfig();
        $scope.onChangePageIndex();
    }
    
    $scope.numberInputTypeKeyPress = function ($event) {
        if (($event.keyCode >= 48 && $event.keyCode <= 57) || ($event.keyCode >= 96 && $event.keyCode <= 105) || $event.keyCode == 8 || $event.keyCode == 46) {

            var incurredCost = 0;

            if ($scope.incurredCost != null) {
                incurredCost = $scope.incurredCost;
            }

            $scope.finalTotalAmount += incurredCost;
        }
    }
});