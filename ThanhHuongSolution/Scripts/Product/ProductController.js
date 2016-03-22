var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.bootstrap']);
app.controller('ProductController', function ($scope, toastr, $http) {

    $scope.productTypes = [{ 'key': 'Tất cả', 'value': '0' }, { 'key': 'Phân bón', 'value': '1' }, { 'key': 'Gạo', 'value': '2' }, { 'key': 'Cà phê', 'value': '3' }];
    $scope.newProductTypes = [{ 'key': 'Phân bón', 'value': '1' }, { 'key': 'Gạo', 'value': '2' }, { 'key': 'Cà phê', 'value': '3' }];
    $scope.newUnitTypes = [{ 'key': 'Kg', 'value': '1' }];
    $scope.pageSize = 10;
    $scope.pageIndex = 1;
    $scope.maxsize = 5;
    $scope.pagingSource = [];
    $scope.query = '';

    // Declare a proxy to reference the hub.
    var signalRHub = $.connection.signalRHub;
    // Create a function that the hub can call to broadcast messages.
    signalRHub.client.broadcastMessage = function () {
    
        $http.post("/Product/Search", {query:''})
        .success(function (response) {
            $scope.availableProduct = response.data;
            $scope.pagingSource = $scope.availableProduct;

            $scope.updatePagingConfig();

            $scope.onChangePageIndex();
            toastr.success("Dữ liệu được cập nhật lại.");
        });
    };

    $.connection.hub.start().done(function () { // start hub

        $scope.saveProduct = function () {
            var file = $('#productImage')[0];
            var form = new FormData();
            form.append("productImage", file.files[0]);
            form.append("Id", $scope.productId);
            form.append("TrackingNumber", $scope.trackingNumber);
            form.append("Name", $scope.name);
            form.append("Description", $scope.description);
            form.append("UnitType", $scope.newSelectedUnitType.value);
            form.append("ProductType", $scope.newSelectedProductType.value);
            form.append("WholesalePrice", $scope.wholesalePrice);
            form.append("RetailPrice", $scope.retailPrice);
            form.append("Number", $scope.number);

            $http.post("/Product/SaveProduct", form, {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            }).success(function (response) {
                if (response.isSuccess) {
                    $scope.search();
                    toastr.success('Lưu thông tin sản phẩm thành công');
                    signalRHub.server.sendToUpdatePrice();
                }
                else {
                    toastr.error('error at: ' + response.message);
                }
            });
        }
    });

    $scope.init = function (data) {
        $scope.availableProduct = data.LstProduct;
        $scope.pagingSource = $scope.availableProduct;
        $scope.selectedProductType = $scope.productTypes[0];
        $scope.newSelectedProductType = $scope.newProductTypes[0];
        $scope.newSelectedUnitType = $scope.newUnitTypes[0];
        
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

    $scope.getProductType = function (type) {
        return $scope.newProductTypes[type - 1];
    }

    $scope.getUnitType = function (type) {
        switch (type) {
            case 1: return $scope.newUnitTypes[0];
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
        if ($scope.numPages == 0)
            return;

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

    $scope.promptDeleteProduct = function (productId) {
        $scope.productIdtoDelete = productId;
    }

    $scope.deleteProduct = function (productId)
    {
        $http.post("/Product/Delete", {productId : productId}).
        success(function (response)
        {
            if (response.isSuccess)
            {
                $scope.search();

                toastr.success("Xoá sản phẩm thành công");
            }
            else
            {
                toastr.error(response.message);
            }
        });
    }

    $scope.viewProduct = function (productId)
    {
        $scope.productId = productId;

        if (productId != '') {
            for (var i = 0; i < $scope.products.length; i++) {
                if ($scope.products[i].Id == productId) {
                    $scope.trackingNumber = $scope.products[i].TrackingNumber;
                    $scope.name = $scope.products[i].Name;
                    $scope.description = $scope.products[i].Description;
                    $scope.newSelectedProductType = $scope.getProductType($scope.products[i].ProductType);
                    $scope.newSelectedUnitType = $scope.getUnitType($scope.products[i].UnitType);
                    $scope.wholesalePrice = $scope.products[i].WholesalePrice;
                    $scope.retailPrice = $scope.products[i].RetailPrice;
                    $scope.number = $scope.products[i].Number;
                    return;
                }
            }
        }
        else {
            $scope.trackingNumber = '';
            $scope.name = '';
            $scope.description = '';
            $scope.newSelectedProductType = $scope.newProductTypes[0];
            $scope.newSelectedUnitType = $scope.newUnitTypes[0];
            $scope.wholesalePrice = '';
            $scope.retailPrice = '';
            $scope.number = 0;

            $scope.form_product_details.$setPristine();
        }
    }

    $scope.chooseFile = function () {
        $("#productImage").click();
    }

    $scope.changeImage = function (chooseFileElement, imageElement) {
        var chooseFileControl = new FileControl();
        chooseFileControl.BindingImage(chooseFileElement, document.getElementById("showImage"))
    }
});