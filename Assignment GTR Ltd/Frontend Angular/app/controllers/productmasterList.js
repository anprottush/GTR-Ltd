var app = angular.module('my-app', []);
app.controller('productmasterlistCtrl',function($scope,$http){
    var apiUrl="https://localhost:7180/api/productmaster/all";
    $http.get(apiUrl)
    .then(function(res) {
        $scope.productmasters = res.data;
        console.log(res.data);
    })
    .catch(function(err) {
        console.log(err);
        console.error('Error fetching list:', err);
    });

});


app.controller('productmasterDeleteCtrl',function($scope,$http){   
    $scope.deleteproductmaster = function(id){
      $http.get("https://localhost:7180/api/productmaster/delete/"+id).then(function(resp){
          alert("Data Deleted");
      },function(err){
          alert(err);
      });
  }; 
});