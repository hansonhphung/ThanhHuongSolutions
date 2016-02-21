var app = angular.module('ThanhHuongSolution', ['toastr']);

app.controller('AccountController', function ($scope, toastr, $http) {

    $scope.login = function ()
    {
        

        $.ajax({
            url: "/Account/Login",
            data: { username: $scope.username, password: $scope.password }
        }).success(function (response) {
            location.href = "/Billing/List";
        });
    }

});
