var app = angular.module('ThanhHuongSolution', ['toastr']);

app.controller('BillingController', function ($scope, toastr, $http) {

    $scope.init = function (data) {
        //alert(JSON.stringify(data));
        $scope.lstBilling = data.LstBilling;
        $scope.bills = data.LstBilling;
    }

    $scope.search = function () {
        $http.post("/Billing/Search", { query: $scope.query }, {
        }).success(function (response) {
            if (response.isSuccess) {
                var data = response.data;

                $scope.bills = data;
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
    }

    $scope.searchKeyDown = function (event) {
        if (event.keyCode == 13) {
            $scope.search();
        }
    }

    $scope.viewBill = function (billId)
    {
        $scope.billId = billId;
        if (billId != '')
        {
            for (var i = 0; i < $scope.bills.length; i++)
            {
                if ($scope.bills[i].Id == billId)
                {
                    $scope.trackingNumber = $scope.bills[i].TrackingNumber;
                    $scope.customerId = $scope.bills[i].Customer.Id;
                    $scope.customerTrackingNumber = $scope.bills[i].Customer.CustomerTrackingNumber;
                    $scope.customerName = $scope.bills[i].Customer.CustomerName;
                    $scope.totalAmount = $scope.bills[i].TotalAmount;
                    $scope.billCreatedDate = $scope.bills[i].BillCreatedDate;
                    $scope.createdAt = $scope.bills[i].CreatedAt;
                    $scope.cart = $scope.bills[i].Cart;                    
                    return;
                }
            }
        }
    }
});