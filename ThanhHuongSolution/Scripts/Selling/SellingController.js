var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.select', 'ui.bootstrap']);
app.controller('SellingController', function ($scope, toastr, $http) {

    $scope.shoppingCart = [];
    $scope.pagingSource = [];
    $scope.needUpdateProduct = [];

    $scope.maxSize = 1;
    $scope.recordPerPage = 1;
    $scope.pageIndex = 1;
    $scope.totalAmount = 0;
    $scope.payAmount = 0;

    // true  Custom Price choosen  false, use standard price
    $scope.retailPriceChoosen = true;

    // Declare a proxy to reference the hub.
    var signalRHub = $.connection.signalRHub;
    // Create a function that the hub can call to broadcast messages.
    signalRHub.client.broadcastMessage = function () {
    
        $http.post("/Product/GetAllProduct")
        .success(function (response) {
            $scope.lstProduct = response.data;
            toastr.success("Dữ liệu được cập nhật lại.");
        });
    };

    signalRHub.client.updatePrice = function () {
        $http.post("/Product/GetAllProduct")
        .success(function (response) {
            $scope.lstProduct = response.data;

            for (var i = 0; i < $scope.pagingSource.length; i++)
            {
                for(var j = 0; j < $scope.lstProduct.length; j++)
                {
                    if ($scope.pagingSource[i].TrackingNumber == $scope.lstProduct[j].TrackingNumber)
                    {
                        $scope.totalAmount -= $scope.pagingSource[i].TotalPrice;
                        $scope.pagingSource[i].TotalPrice = $scope.pagingSource[i].Number * $scope.lstProduct[j].RetailPrice;
                        $scope.totalAmount += $scope.pagingSource[i].TotalPrice;
                        break;
                    }
                }
            }

            for (var i = 0; i < $scope.lstProduct.length; i++)
            {
                if ($scope.lstProduct[i].TrackingNumber == $scope.selectedProduct.TrackingNumber)
                {
                    $scope.selectedProduct = $scope.lstProduct[i];
                    $scope.wholesalePrice = $scope.selectedProduct.WholesalePrice;
                    $scope.retailPrice = $scope.selectedProduct.RetailPrice;
                    break;
                }
            }

            var payAmount = 0;

            if ($scope.payAmount != null) {
                payAmount = $scope.payAmount;
            }

            if (payAmount >= $scope.totalAmount)
                $scope.liabilityAmount = 0;
            else
                $scope.liabilityAmount = $scope.totalAmount - payAmount;

            $scope.onChangePageIndex();

            toastr.success("Dữ liệu được cập nhật lại.");
        });
    }

    $.connection.hub.start().done(function () { // start hub

        $scope.createBilling = function () {
            if ($scope.selectedCustomer == null) {
                toastr.warning("Vui lòng chọn khách hàng.");
                return;
            }

            if ($scope.shoppingCart.length == 0) {
                toastr.warning("Hoá đơn chưa có mặt hàng.");
                return;
            }


            for (var i = 0; i < $scope.pagingSource.length; i++)
            {
                for (var j = 0; j < $scope.lstProduct.length; j++) {
                    if ($scope.pagingSource[i].TrackingNumber == $scope.lstProduct[j].TrackingNumber) {
                        if ($scope.pagingSource[i].Number > $scope.lstProduct[j].Number) {
                            toastr.error("Số lượng còn lại của mặt hàng " + $scope.pagingSource[i].Name + " không đủ. Còn tồn " + $scope.lstProduct[j].Number);
                            return;
                        }

                        $scope.needUpdateProduct.push({ ProductTrackingNumber: $scope.pagingSource[i].TrackingNumber, ProductRemainingNumber: $scope.lstProduct[j].Number - $scope.pagingSource[i].Number });
                    }
                }
            }

            $http.post("/Selling/UpdateListSellingProduct", { lstProductInfo: $scope.needUpdateProduct }, {
            }).success(function (response) {
                if (response.isSuccess) {

                    var form = new FormData();
                    form.append("Customer.CustomerId", $scope.selectedCustomer.CustomerId);
                    form.append("Customer.CustomerTrackingNumber", $scope.selectedCustomer.CustomerTrackingNumber);
                    form.append("Customer.CustomerName", $scope.selectedCustomer.CustomerName);

                    if ($scope.pagingSource != null && $scope.pagingSource.length > 0) {
                        for (var i = 0; i < $scope.pagingSource.length; i++) {
                            form.append("Cart[" + i + "].ProductTrackingNumber", $scope.pagingSource[i].TrackingNumber);
                            form.append("Cart[" + i + "].ProductName", $scope.pagingSource[i].Name);
                            form.append("Cart[" + i + "].Number", $scope.pagingSource[i].Number);
                            form.append("Cart[" + i + "].Price", $scope.pagingSource[i].TotalPrice);
                        }
                    }

                    form.append("TotalAmount", $scope.totalAmount);

                    $http.post("/Selling/CreateBilling", form, {
                        withCredentials: true,
                        headers: { 'Content-Type': undefined },
                        transformRequest: angular.identity
                    }).success(function (response) {
                        if (response.isSuccess) {

                            toastr.success('Tạo hoá đơn thành công');
                            
                            if ($scope.payAmount < $scope.totalAmount) //Need to create debt
                            {
                                var debt = {
                                    Customer: {
                                        CustomerId: $scope.selectedCustomer.CustomerId,
                                        CustomerTrackingNumber: $scope.selectedCustomer.CustomerTrackingNumber,
                                        CustomerName: $scope.selectedCustomer.CustomerName
                                    },
                                    TotalAmount: $scope.totalAmount - $scope.payAmount,
                                    DebtCreatedDate : $scope.createBillDate
                                };

                                $http.post("/Selling/CreateDebt", { debt: debt }, {
                                }).success(function (response) {

                                    //update the DEBT of its customer by calling API to update customer DEBT
                                    var form = new FormData();
                                    form.append("Id", $scope.selectedCustomer.CustomerId);
                                    form.append("DebtAmount", $scope.totalAmount - $scope.payAmount);
                                    form.append("IsIncDebt", true);

                                    $http.post("/Customer/UpdateCustomerDebt", form, {
                                        withCredentials: true,
                                        headers: { 'Content-Type': undefined },
                                        transformRequest: angular.identity
                                    }).success(function (response) {
                                        $scope.initData();
                                        signalRHub.server.send();
                                    });
                                });
                            }
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

        var price = 0;
        if ($scope.retailPriceChoosen == true)
        {
            price = $scope.retailPrice; // price of per item
        }
        else
        {
            price = $scope.wholesalePrice; // price of per item
        } 

        var isExist = false;

        for (var i = 0; i < $scope.pagingSource.length; i++)
        {
            if ($scope.pagingSource[i].TrackingNumber == $scope.selectedProduct.TrackingNumber) //already in cart
            {
                $scope.pagingSource[i].Number += $scope.number;
                $scope.pagingSource[i].TotalPrice += $scope.pagingSource[i].Price * $scope.number;
                isExist = true;
                break;
            }
        }

        if (!isExist)
        {
            $scope.pagingSource.push({ TrackingNumber: $scope.selectedProduct.TrackingNumber, Name: $scope.selectedProduct.Name, Price: price ,Number: $scope.number, TotalPrice: price * $scope.number });
        }

        $scope.totalAmount += price * $scope.number;

        var payAmount = 0;

        if ($scope.payAmount != null) {
            payAmount = $scope.payAmount;
        }

        if (payAmount >= $scope.totalAmount)
            $scope.liabilityAmount = 0;
        else
            $scope.liabilityAmount = $scope.totalAmount - payAmount;

        $scope.updatePagingConfig();
        $scope.onChangePageIndex();
    }

    $scope.viewItemInCart = function (trackingNumber)
    {
        for (var i = 0; i < $scope.pagingSource.length; i++)
        {
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
        for (var i = 0; i < $scope.pagingSource.length; i++)
        {
            if ($scope.pagingSource[i].TrackingNumber == trackingNumber)
            {
                $scope.totalAmount -= $scope.pagingSource[i].TotalPrice;
                $scope.pagingSource.splice(i, 1);
            }
        }

        var payAmount = 0;

        if ($scope.payAmount != null) {
            payAmount = $scope.payAmount;
        }

        if (payAmount >= $scope.totalAmount)
            $scope.liabilityAmount = 0;
        else
            $scope.liabilityAmount = $scope.totalAmount - payAmount;

        $scope.updatePagingConfig();
        $scope.onChangePageIndex();
    }

    $scope.saveItemInfo = function ()
    {
        for (var i = 0; i < $scope.pagingSource.length; i++)
        {
            if ($scope.pagingSource[i].TrackingNumber == $scope.itemTrackingNumber)
            {
                $scope.pagingSource[i].Number = $scope.itemQuantity;
                $scope.totalAmount -= $scope.pagingSource[i].TotalPrice;
                $scope.pagingSource[i].TotalPrice = $scope.itemTotalPrice;
                $scope.totalAmount += $scope.pagingSource[i].TotalPrice;

                var payAmount = 0;

                if ($scope.payAmount != null) {
                    payAmount = $scope.payAmount;
                }

                if (payAmount >= $scope.totalAmount)
                    $scope.liabilityAmount = 0;
                else
                    $scope.liabilityAmount = $scope.totalAmount - payAmount;

                break;
            }
        }

        $scope.onChangePageIndex();
    }

    $scope.initData = function ()
    {
        $scope.shoppingCart = [];

        $scope.selectedProduct = $scope.lstProduct[0];

        $scope.selectedCustomer = $scope.lstCustomer[0];

        $scope.wholesalePrice = $scope.selectedProduct.WholesalePrice;

        $scope.retailPrice = $scope.selectedProduct.RetailPrice;

        $scope.totalAmount = 0;

        $scope.number = 0;

        $scope.liabilityAmount = 0;

        $scope.payAmount = 0;
    }

    $scope.cancel = function ()
    {
        $scope.initData();
    }

    $scope.updatePagingConfig = function () {
        $scope.totalProduct = $scope.pagingSource.length;
        $scope.numPages = Math.ceil($scope.totalProduct / $scope.recordPerPage);
    }

    $scope.onChangePageIndex = function ()
    {
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

    $scope.numberInputTypeKeyPress = function ($event)
    {
        if (($event.keyCode >= 48 && $event.keyCode <= 57) ||($event.keyCode >= 96 && $event.keyCode <= 105 ) || $event.keyCode == 8 || $event.keyCode == 46)
        {

            var payAmount = 0;

            if ($scope.payAmount != null)
            {
                payAmount = $scope.payAmount;
            }
                
            if (payAmount >= $scope.totalAmount)
                $scope.liabilityAmount = 0;
            else
                $scope.liabilityAmount = $scope.totalAmount - payAmount;
        }
    }

    $scope.quantityInputTypeKeyPress = function ($event) {
        if (($event.keyCode >= 48 && $event.keyCode <= 57) || ($event.keyCode >= 96 && $event.keyCode <= 105) || $event.keyCode == 8 || $event.keyCode == 46) {

            var quantity = 0;

            if ($scope.itemQuantity != null) {
                quantity = $scope.itemQuantity;
            }

            $scope.itemTotalPrice = quantity * $scope.itemPrice;
        }
    }
});