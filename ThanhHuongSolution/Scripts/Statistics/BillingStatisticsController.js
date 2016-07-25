var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.bootstrap']);

app.controller('BillingStatisticsController', function ($scope, toastr, $http) {

    var BILL = 'BILL';
    var RECEIVING_BILL = 'RECEIVING_BILL';
    var CREATED_DATE_DT = 'CreatedAt_DT';

    $scope.init = function () {
        $scope.pageIndex = 1;
        $scope.recordPerPage = 10;
        $scope.maxSize = 5;
        $scope.bills = [];
        $scope.sortBy = CREATED_DATE_DT;
        $scope.sortDirection = false;
        $scope.isSearchName = false;
        $scope.query = '';
        $scope.totalCost = 0;
        $scope.fromDate = null;
        $scope.toDate = null;

        // true : BILL, false : RECEIVING_BILL
        $scope.billType = BILL;
    }

    //change date format from dd/MM/yyyy to MM/dd/yyyy
    //because new Date in js need format MM/dd/yyyy
    $scope.formatDate = function(date)
    {
        var s_date = date.split("/");
        var ret = s_date[1] + '/' + s_date[0] + '/' + s_date[2];
        return ret;
    }

    $scope.search = function () {
        $http.post("/Statistics/SearchBillInDateRange", { query: '', fromDate: $scope.fromDate, toDate: $scope.toDate, pagination: { PageIndex: $scope.pageIndex, PageSize: $scope.PageSize, SortBy: $scope.sortBy, SortDirection: $scope.sortDirection }, billType: $scope.billType }, {
        }).success(function (response) {
            if (response.isSuccess) {
                var data = response.data.LstBilling;

                $scope.bills = data;
                $scope.totalBillings = response.data.TotalItem;
                $scope.totalCost = response.data.TotalCost;
            }
            else {
                toastr.error('error at: ' + response.message);
            }
        });
    }

    $scope.viewBill = function (billId) {
        $scope.billId = billId;
        if (billId != '') {
            for (var i = 0; i < $scope.bills.length; i++) {
                if ($scope.bills[i].Id == billId) {
                    $scope.trackingNumber = $scope.bills[i].TrackingNumber;
                    $scope.totalAmount = $scope.bills[i].TotalAmount;
                    $scope.billCreatedDate = $scope.bills[i].BillCreatedDate;
                    $scope.createdAt = $scope.bills[i].CreatedAt;
                    $scope.cart = $scope.bills[i].Cart;

                    if ($scope.billType == 'BILL') {
                        $scope.customerId = $scope.bills[i].Customer.Id;
                        $scope.customerTrackingNumber = $scope.bills[i].Customer.CustomerTrackingNumber;
                        $scope.customerName = $scope.bills[i].Customer.CustomerName;
                    }

                    if ($scope.billType == 'RECEIVING_BILL') {
                        $scope.incurredCost = $scope.bills[i].IncurredCost;
                        $scope.finalTotalAmount = $scope.bills[i].FinalTotalAmount;
                    }
                    return;
                }
            }
        }
    }

    $scope.onChangePageIndex = function () {
        $scope.search();
    }

    $scope.switchSellingBill = function () {
        $scope.billType = BILL;
        $scope.totalCost = 0;
        $scope.bills = [];
        $scope.totalBillings = 0;
        //$scope.search();
    }

    $scope.switchReceivingBill = function () {
        $scope.billType = RECEIVING_BILL;
        $scope.totalCost = 0;
        $scope.bills = [];
        $scope.totalBillings = 0
        //$scope.search();
    }

    $scope.doStatistics = function ()
    {
        if ($('#fromDate').val() == null || $('#fromDate').val() == '')
        {
            toastr.warning("Vui lòng chọn ngày bắt đầu thống kê");
            return;
        }

        if ($('#toDate').val() == null || $('#toDate').val() == '') {
            toastr.warning("Vui lòng chọn ngày kết thúc thống kê");
            return;
        }

        var fromDate_preFormat = $('#fromDate').val();
        var s_fromDate = $scope.formatDate(fromDate_preFormat);
        var fromDate = new Date(s_fromDate);

        var toDate_preFormat = $('#toDate').val();
        var s_toDate = $scope.formatDate(toDate_preFormat);
        var toDate = new Date(s_toDate);

        if (toDate < fromDate)
        {
            toastr.warning("Ngày bắt đầu phải trước ngày kết thúc thống kê");
            return;
        }

        $scope.toDate = toDate;
        $scope.fromDate = fromDate;


        $scope.search();
        $scope.totalCost = 0;
        for (var i = 0; i < $scope.bills.length; i++)
        {
            if ($scope.billType == BILL)
                $scope.totalCost += $scope.bills[i].TotalAmount;
            else
                $scope.totalCost += $scope.bills[i].FinalTotalAmount;
        }
    }
});