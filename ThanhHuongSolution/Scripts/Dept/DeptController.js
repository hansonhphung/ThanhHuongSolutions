var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.bootstrap']);
app.controller('DebtController', function ($scope, toastr, $http){
    
    $scope.init = function (data) {
        //Pagination
        $scope.pageIndex = 1;
        $scope.recordPerPage = 10;
        $scope.maxSize = 5;
        $scope.pagingSource = [];
        $scope.currentIndex = 1;
        $scope.isSearchName = true;
        $scope.mode = 'DEBT';
        $scope.query = '';
        $scope.searchCustomerId = '';
        $scope.sortBy = 'CreatedAt';
        $scope.sortDirection = false;

        $http.post("/Debt/Search", { customerId: $scope.searchCustomerId, query: $scope.query, pagination: { PageIndex: $scope.pageIndex, PageSize: $scope.recordPerPage, SortBy: $scope.sortBy, SortDirection: $scope.sortDirection }, debtType: $scope.mode }, {
        }).success(function (response) {
            if (response.isSuccess) {
                var data = response.data.LstDebt;
                $scope.lstDebt = data;
                $scope.totalDebts = response.data.TotalItem;
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });

        $('#search_date').trigger('input');
    }

    $scope.changeSearchMode = function()
    {
        $scope.isSearchName = !$scope.isSearchName;
        $scope.query = '';
    }

    $scope.switchDebtMode = function ()
    {
        $scope.mode = 'DEBT';
        $scope.pageIndex = 1;
        $scope.search();
    }

    $scope.searchKeyDown = function (event) {
        if (event.keyCode == 13) {
            $scope.pageIndex = 1;
            $scope.search();
        }
    }

    $scope.switchPaidDebtMode = function ()
    {
        $scope.mode = 'PAID_DEBT';
        $scope.pageIndex = 1;
        $scope.search();
    }

    $scope.search = function ()
    {
        $http.post("/Debt/Search", { customerId: $scope.searchCustomerId, query: $scope.query, pagination: { PageIndex: $scope.pageIndex, PageSize: $scope.recordPerPage, SortBy: $scope.sortBy, SortDirection: $scope.sortDirection }, debtType: $scope.mode}, {
        }).success(function (response) {
            if (response.isSuccess) {
                var data = response.data.LstDebt;

                $scope.lstDebt = data;
                $scope.totalDebts = response.data.TotalItem;
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
    }

    $scope.viewBill = function (billTrackingNumber) {
        
        $http.post("/Billing/GetBillByTrackingNumber", { trackingNumber: billTrackingNumber }, {
        }).success(function (response) {

            var data = response.data;

            $scope.trackingNumber = data.TrackingNumber;
            $scope.customerId = data.Customer.Id;
            $scope.customerTrackingNumber = data.Customer.CustomerTrackingNumber;
            $scope.customerName = data.Customer.CustomerName;
            $scope.totalAmount = data.TotalAmount;
            $scope.billCreatedDate = data.BillCreatedDate;
            $scope.cart = data.Cart;
        });
    }

    $scope.onChangePageIndex = function () {
        $scope.search();
    }
});