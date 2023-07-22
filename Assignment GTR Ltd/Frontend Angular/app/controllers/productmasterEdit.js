var app = angular.module('my-app', []);
app.controller('masterCtrl',function($scope,$http,$routeParams){
    //$scope.Id = $routeParams.Id;
    console.log($routeParams.Id);
    // $http.get("").then(function(res){
    //     $scope.Id = res.data;
    //     console.log(res.data);
    // },function(err){
    //     console.log(err);
    // });
  });