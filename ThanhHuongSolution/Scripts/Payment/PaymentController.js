var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.select']);
app.controller('PaymentController', function ($scope, toastr, $http) {

    //Pagination
    $scope.pageIndex = 1;
    $scope.recordPerPage = 3;
    $scope.maxSize = 5;
    $scope.pagingSource = [];
    $scope.currentIndex = 1;
    $scope.isSearchName = true;

    $scope.init = function (data) {
        $scope.lstCustomer = data.LstCustomer;

        $scope.createBillDate = data.CreateBillDate;
        $scope.liabilityAmount = 0;
    }

    $scope.selectCustomer = function (selectedCustomer) {
        $scope.selectedCustomer = selectedCustomer;

        $scope.liabilityAmount = selectedCustomer.LiabilityAmount;
    }

    $scope.updateCustomerDept = function () {

        $scope.customerId = '';

        if ($scope.selectedCustomer == null || $scope.selectedCustomer == undefined) {
            toastr.warning("Vui lòng chọn khách hàng.");
            return;
        }
        else {
            $scope.customerId = $scope.selectedCustomer.CustomerId;
        }

        if ($scope.paidAmount == null || $scope.paidAmount <= 0) {
            toastr.warning("Vui lòng nhập số tiền thanh toán.");
            return;
        }

        var form = new FormData();
        form.append("Id", $scope.customerId);
        form.append("DebtAmount", $scope.paidAmount);
        form.append("IsIncDebt", false);

        $http.post("/Customer/UpdateCustomerDebt", form, {
            withCredentials: true,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        }).success(function (response) {
            if (response.isSuccess) {
                var paidDebt = {
                    Customer: {
                        CustomerId: $scope.selectedCustomer.CustomerId,
                        CustomerTrackingNumber: $scope.selectedCustomer.CustomerTrackingNumber,
                        CustomerName: $scope.selectedCustomer.CustomerName
                    },
                    TotalAmount: $scope.paidAmount,
                    DebtCreatedDate: $scope.createBillDate
                };

                $http.post("/Debt/CreatePaidDebt", { debt: paidDebt }, {
                }).success(function (response) {
                    toastr.success('Thanh toán nợ thành công');
                    $scope.refreshData();                    
                });

            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
    }

    $scope.refreshData = function () {
        $scope.selectedCustomer = null;
        $scope.liabilityAmount = 0;
        $scope.paidAmount = 0;

        $http.post("/Payment/GetCustomerInfoAndDebt", null, {
            withCredentials: true,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        }).success(function (response) {
            if (response.isSuccess) {
                var data = response.data;
                $scope.lstCustomer = data;
                
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
    }
});