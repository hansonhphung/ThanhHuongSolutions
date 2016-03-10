var app = angular.module('ThanhHuongSolution', ['toastr','ui.select']);
app.controller('PaymentController', function ($scope, toastr, $http) {

    //Pagination
    $scope.pageIndex = 1;
    $scope.recordPerPage = 3;
    $scope.maxSize = 5;
    $scope.pagingSource = [];
    $scope.currentIndex = 1;
    $scope.isSearchName = true;

    $scope.init = function (data)
    {
        $scope.lstCustomer = data.LstCustomer;

        $scope.selectedCustomer = $scope.lstCustomer[0];
    }

    $scope.selectCustomer = function (selectedCustomer)
    {
        $scope.selectedCustomer = selectedCustomer;
    }

    $scope.updateCustomerDept = function () {

        $scope.customerId = "56c772a4c185aa0c98ab251d";
        $scope.debtAmount = '2000000';

        var form = new FormData();
        form.append("Id", $scope.customerId);
        form.append("DebtAmount", $scope.debtAmount);
        form.append("IsIncDebt", false);

        $http.post("/Customer/UpdateCustomerDebt", from, {
            withCredentials: true,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        }).success(function (response) {
            if (response.isSuccess) {
                toastr.success('Thanh toán nợ thành công');
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });

    }
});