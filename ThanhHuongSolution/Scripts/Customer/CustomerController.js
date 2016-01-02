var app = angular.module('ThanhHuongSolution', ['toastr']);
app.controller('CustomerController', function ($scope, toastr) {
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
            $scope.liabilityAmount = '';
        }
    }

    $scope.search = function()
    {
        var query = $scope.query;

        $.ajax({
            type: "POST",
            url: "/Customer/Search",
            data: { query: query },
            success: function (response) {
                if (response.isSuccess) {
                    var data = response.data;
                    $scope.customers = data;
                }
                else {
                    alert('error at: ' + response.message);
                }
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
        alert(customerId);
        $.ajax({
            type: "POST",
            url: "/Customer/Delete",
            data: { customerId: customerId },
            success: function (response) {
                if (response.isSuccess) {
                    toastr.success('Xoá khách hàng thành công');
                    $scope.search();
                }
                else {
                    alert('error at: ' + response.message);
                }
            }
        });
    }
});