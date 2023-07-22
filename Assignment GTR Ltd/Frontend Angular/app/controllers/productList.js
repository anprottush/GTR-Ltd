var app = angular.module('my-app', []);
app.controller('productlistCtrl',function($scope,$http){
    var authToken = localStorage.getItem('Token');
    //$http.defaults.headers.common['Authorization'] = 'Bearer ' + authToken;
    var apiUrl="https://www.pqstec.com/InvoiceApps/values/GetProductListAll";
    apiUrl += '?token=' + authToken;
    $http.get(apiUrl)
    .then(function(res) {
        $scope.isAuthorized = true;
        $scope.products = res.data;
        console.log(res.data);
        
    })
    .catch(function(err) {
        if (err.status === 401) {
            $scope.isAuthorized = false;
          } else {
            console.log(err);
            console.error('Error fetching product list:', err);
          }
     
    });

});