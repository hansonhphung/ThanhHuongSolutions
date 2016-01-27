var app = angular.module('ThanhHuongSolution', ['toastr', 'ui.select']);
app.controller('SellingController', function ($scope, toastr, $http) {
    $scope.employee = {};

    $scope.employees = [
  { name: 'Alpha', subtitle: 'Alice', group: 'Group1' },
  { name: 'Bravo', subtitle: 'Bob', group: 'Group2' },
  { name: 'Charlie', subtitle: 'Carl', group: 'Group1' },
  { name: 'Golf', subtitle: 'George', group: 'Group1' },
  { name: 'Hotel', subtitle: 'Harry', group: 'Group1' },
  { name: 'Juliet', subtitle: 'Joe', group: 'Group2' },
  { name: 'Kilo', subtitle: 'Kate', group: 'Group2' },
  { name: 'Lima', subtitle: 'Laura', group: 'Group2' }
    ];
});