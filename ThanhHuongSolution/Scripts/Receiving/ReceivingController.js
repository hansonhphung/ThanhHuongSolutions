var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.select', 'ui.bootstrap']);
app.controller('ReceivingController', function ($scope, toastr, $http) {

    $scope.shoppingCart = [];
    $scope.pagingSource = [];
    $scope.needUpdateProduct = [];

    $scope.maxSize = 3;
    $scope.recordPerPage = 5;
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

        if ($scope.pagingSource.length == 0)
            return;

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

            $scope.finalTotalAmount = $scope.totalAmount + incurredCost;
        }
    }

    $scope.initData = function () {
        $scope.shoppingCart = [];

        $scope.pagingSource = [];

        $scope.selectedProduct = $scope.lstProduct[0];

        $scope.inputPrice = 0;

        $scope.totalAmount = 0;

        $scope.finalTotalAmount = 0;

        $scope.number = 0;

        $scope.incurredCost = 0;

        $scope.isDisablePrice = false;

        $scope.updatePagingConfig();
    }

    $scope.createReceivingBill = function () {
        if ($scope.shoppingCart.length == 0) {
            toastr.warning("Hoá đơn chưa có mặt hàng.");
            return;
        }


        for (var i = 0; i < $scope.pagingSource.length; i++) {
            for (var j = 0; j < $scope.lstProduct.length; j++) {
                if ($scope.pagingSource[i].TrackingNumber == $scope.lstProduct[j].TrackingNumber) {
                    $scope.needUpdateProduct.push({ ProductTrackingNumber: $scope.pagingSource[i].TrackingNumber, ProductRemainingNumber: $scope.lstProduct[j].Number + $scope.pagingSource[i].Number });
                }
            }
        }

        $http.post("/Selling/UpdateListSellingProduct", { lstProductInfo: $scope.needUpdateProduct }, {
        }).success(function (response) {
            if (response.isSuccess) {

                var form = new FormData();
                /*
                form.append("Customer.CustomerId", $scope.selectedCustomer.CustomerId);
                form.append("Customer.CustomerTrackingNumber", $scope.selectedCustomer.CustomerTrackingNumber);
                form.append("Customer.CustomerName", $scope.selectedCustomer.CustomerName);
                */

                if ($scope.pagingSource != null && $scope.pagingSource.length > 0) {
                    for (var i = 0; i < $scope.pagingSource.length; i++) {
                        form.append("Cart[" + i + "].ProductTrackingNumber", $scope.pagingSource[i].TrackingNumber);
                        form.append("Cart[" + i + "].ProductName", $scope.pagingSource[i].Name);
                        form.append("Cart[" + i + "].Number", $scope.pagingSource[i].Number);
                        form.append("Cart[" + i + "].Price", $scope.pagingSource[i].TotalPrice);
                    }
                }

                form.append("TotalAmount", $scope.totalAmount);
                form.append("IncurredCost", $scope.incurredCost);
                form.append("FinalTotalAmount", $scope.finalTotalAmount);

                $http.post("/Receiving/CreateReceivingBill", form, {
                    withCredentials: true,
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                }).success(function (response) {
                    if (response.isSuccess) {

                        toastr.success('Tạo hoá đơn nhập hàng thành công');

                        $scope.initData();
                    }
                    else {
                        toastr.error('error at: ' + response.message);
                    }
                });
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
    }
});