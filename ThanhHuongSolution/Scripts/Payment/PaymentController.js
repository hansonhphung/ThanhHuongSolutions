var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.bootstrap']);
app.controller('PaymentController', function ($scope, toastr, $http) {

    //Pagination
    $scope.pageIndex = 1;
    $scope.recordPerPage = 3;
    $scope.maxSize = 5;
    $scope.pagingSource = [];
    $scope.currentIndex = 1;
    $scope.isSearchName = true;
    $scope.mode = 'debt';

    $scope.changeSearchMode = function () {
        $scope.isSearchName = !$scope.isSearchName;
    }

    $scope.switchDebtMode = function () {
        $scope.mode = 'debt';
    }

    $scope.switchPaidDebtMode = function () {
        $scope.mode = 'paiddebt';
    }

    $scope.createPayDebt = function () {

    }

    $scope.updateCustomerDept = function () {

        $scope.query = '';

        var form = new FromData();
        form.append("Id", $scope.customerId);
        form.append("DebtAmount", $scope.debtAmount);

        $http.post("/Customer/UpdateCustomerDept", from, {
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