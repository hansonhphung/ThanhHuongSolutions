var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.bootstrap']);
app.controller('CustomerController', function ($scope, toastr, $location, $http) {

    //Pagination
    $scope.pageIndex = 1;
    $scope.recordPerPage = 3;
    $scope.maxSize = 5;
    $scope.pagingSource = [];
    $scope.currentIndex = 1;


    $scope.init = function (data) {
        $scope.availableCustomer = data.LstCustomer;
        $scope.customers = data.LstCustomer;
        $scope.pagingSource = $scope.availableCustomer;
        
        $scope.updatePagingConfig();
        $scope.onChangePageIndex();
    }

    $scope.updatePagingConfig = function ()
    {
        $scope.totalCustomers = $scope.pagingSource.length;
        $scope.numPages = Math.ceil($scope.totalCustomers / $scope.recordPerPage);
    }

    $scope.viewCustomer = function (customerId)
    {
        $scope.customerId = customerId;
        if (customerId != '') {
            for (var i = 0; i < $scope.customers.length; i++) {
                if ($scope.customers[i].Id == customerId) {
                    $scope.trackingNumber = $scope.customers[i].TrackingNumber;
                    $scope.name = $scope.customers[i].Name;
                    $scope.phoneNumber = $scope.customers[i].PhoneNumber;
                    $scope.address = $scope.customers[i].Address;
                    $scope.liabilityAmount = $scope.customers[i].LiabilityAmount;
                    $scope.isVIP = $scope.customers[i].IsVIP;
                    $scope.imgURL = $scope.customers[i].ImgURL;
                    return;
                }
            }
        }
        else {
            $scope.trackingNumber = ''
            $scope.name = '';
            $scope.phoneNumber = '';
            $scope.address = '';
            $scope.liabilityAmount = 0;
            $scope.imgURL = '';
            $scope.isVIP = false;

            $scope.form_customer_details.$setPristine();
        }
    }

    $scope.search = function()
    {
        $scope.query = '';

        $http.post("/Customer/Search", { query: $scope.query })
        .success(function (response) {
            if (response.isSuccess) {
                var data = response.data;

                $scope.availableCustomer = data;
                    
                $scope.searchCustomer();

                $scope.pagingSource = data;
                $scope.updatePagingConfig();
                $scope.pageIndex = $scope.currentIndex;
                $scope.onChangePageIndex();
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
    }

    $scope.customerHistory = function(customerId)
    {
        location.href = "/Billing/List?id=" + customerId;
    }

    $scope.searchCustomer = function ()
    {
        $scope.customers = [];
        var query = $scope.query.toLowerCase();

        for (var i = 0; i < $scope.availableCustomer.length; i++)
        {
            var trackingLower = $scope.availableCustomer[i].TrackingNumber.toLowerCase();
            var nameLower = $scope.availableCustomer[i].Name.toLowerCase();

            if (trackingLower.indexOf(query) !== -1 || nameLower.indexOf(query) !== -1)
                $scope.customers.push($scope.availableCustomer[i]);
        }
    }

    $scope.searchKeyDown = function(event)
    {
        if (event.keyCode == 13)
        {
            $scope.searchCustomer();
        }
    }

    $scope.promptDeleteCustomer = function(customerId)
    {
        $scope.customerIdtoDelete = customerId;
    }

    $scope.deleteCustomer = function (customerId)
    {
        $http.post("/Customer/Delete", { customerId: customerId })
        .success(function (response) {
            if (response.isSuccess) {
                $scope.search();
                toastr.success('Xoá khách hàng thành công');
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
    }

    $scope.setVIPCustomer = function (customerId, isVIP) {

        isVIP = !isVIP;
        
        $http.post("/Customer/SetVIPCustomer", { customerId: customerId, isVIP: isVIP })
        .success(function (response) {
            if (response.isSuccess) {
                $scope.search();
                toastr.success('Cập nhật khách hàng thân thiết thành công');
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
    }

    $scope.onChangePageIndex = function()
    {
        $scope.customers = [];

        if ($scope.pageIndex == $scope.numPages) {
            for (var i = ($scope.pageIndex - 1) * $scope.recordPerPage; i < $scope.pagingSource.length; i++) {
                $scope.customers.push($scope.pagingSource[i]);
            }
        }
        else {
            for (var i = 0; i < $scope.recordPerPage; i++) {
                var index = ($scope.pageIndex - 1) * $scope.recordPerPage + i;
                $scope.customers.push($scope.pagingSource[index]);
            }
        }
        $scope.currentIndex = $scope.pageIndex;
    }

    $scope.saveCustomer = function ()
    {
        var file = $('#customerImage')[0];
        var form = new FormData();
        //form.append("file", this.myFile);
        form.append("customerImage", file.files[0]);
        form.append("Id", $scope.customerId);
        form.append("TrackingNumber", $scope.trackingNumber);
        form.append("Name", $scope.name);
        form.append("PhoneNumber", $scope.phoneNumber);
        form.append("Address", $scope.address);
        form.append("LiabilityAmount", $scope.liabilityAmount);
        form.append("IsVIP", $scope.isVIP);

        $http.post("/Customer/SaveCustomer", form, {
            withCredentials: true,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        }).success(function(response) {
            if (response.isSuccess) {
                $scope.search();
                toastr.success('Lưu thông tin khách hàng thành công');
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
    }

    $scope.chooseFile = function ()
    {
        $("#customerImage").click();
    }

    $scope.changeImage = function (chooseFileElement, imageElement)
    {   
        var chooseFileControl = new FileControl();
        chooseFileControl.BindingImage(chooseFileElement, document.getElementById("showImage"))
    }
});