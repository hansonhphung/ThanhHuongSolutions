var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.bootstrap']);
app.controller('DebtCustomerController', function ($scope, toastr, $location, $http) {

    //Pagination
    $scope.pageIndex = 1;
    $scope.recordPerPage = 10;
    $scope.maxSize = 5;
    $scope.pagingSource = [];
    $scope.currentIndex = 1;
    $scope.totalDebt = 0;

    $scope.init = function () {
        $http.post("/Statistics/SearchDebtCustomer", {query:''})
        .success(function (response) {
            if (response.isSuccess) {
                var data = response.data;
                $scope.availableCustomer = data;
                $scope.customers = data;
                $scope.pagingSource = $scope.availableCustomer;

                $scope.updatePagingConfig();
                $scope.onChangePageIndex();

                for(var i=0;i<$scope.customers.length;i++)
                    $scope.totalDebt += $scope.customers[i].LiabilityAmount;
            }
        });
    }

    $scope.updatePagingConfig = function () {
        $scope.totalCustomers = $scope.pagingSource.length;
        $scope.numPages = Math.ceil($scope.totalCustomers / $scope.recordPerPage);
    }

    $scope.search = function () {
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
                toastr.error(response.message);
            }
        });
    }

    $scope.searchCustomer = function () {
        $scope.customers = [];
        var query = $scope.query.toLowerCase();

        for (var i = 0; i < $scope.availableCustomer.length; i++) {
            var trackingLower = $scope.availableCustomer[i].TrackingNumber.toLowerCase();
            var nameLower = $scope.availableCustomer[i].Name.toLowerCase();

            if (trackingLower.indexOf(query) !== -1 || nameLower.indexOf(query) !== -1)
                $scope.customers.push($scope.availableCustomer[i]);
        }
    }

    $scope.searchKeyDown = function (event) {
        if (event.keyCode == 13) {
            $scope.searchCustomer();
        }
    }

    $scope.onChangePageIndex = function () {
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
});