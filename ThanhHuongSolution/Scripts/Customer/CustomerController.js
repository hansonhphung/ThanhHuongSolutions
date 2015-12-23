var app = angular.module('ThanhHuongSolution', []);
app.controller('CustomerController', function ($scope) {
    $scope.init = function (data) {
        $scope.customers = data.LstCustomer;
    }

    $scope.viewCustomer = function (customerId)
    {
        for (var i = 0; i < $scope.customers.length; i++)
        {
            if ($scope.customers[i].Id == customerId)
            {
                $scope.trackingNumber = $scope.customers[i].TrackingNumber;
                $scope.name = $scope.customers[i].Name;
                $scope.phoneNumber = $scope.customers[i].PhoneNumber;
                $scope.address = $scope.customers[i].Address;
                $scope.liabilityAmount = $scope.customers[i].LiabilityAmount;
                return;
            }
        }
    }
});