var app = angular.module('my-app', []);
app.controller('logoutCtrl',($scope,$http,$window)=>{
    $scope.logout = ()=>{
        localStorage.removeItem("Token");
        localStorage.removeItem("Time");
        alert("logout Successful");
        $window.location.href = 'login.html';
    };
});