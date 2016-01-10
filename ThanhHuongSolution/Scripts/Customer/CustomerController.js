var app = angular.module('ThanhHuongSolution', ['toastr']);
app.controller('CustomerController', function ($scope, toastr, $http) {
    $scope.init = function (data) {
        $scope.availableCustomer = data.LstCustomer;
        $scope.customers = data.LstCustomer;
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
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
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

    $scope.deleteCustomer = function (customerId)
    {
        $http.post("/Customer/Delete", { customerId: customerId})
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

    $scope.saveCustomer = function ()
    {
        var file = $('#File')[0];
        var form = new FormData();
        //form.append("file", this.myFile);
        form.append("file", file.files[0]);
        form.append("Id", $scope.customerId);
        form.append("TrackingNumber", $scope.trackingNumber);
        form.append("Name", $scope.name);
        form.append("PhoneNumber", $scope.phoneNumber);
        form.append("Address", $scope.address);
        form.append("LiabilityAmount", $scope.liabilityAmount);

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
});