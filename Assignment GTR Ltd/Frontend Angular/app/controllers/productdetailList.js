var app = angular.module('my-app', []);
app.controller('productdetaillistCtrl',function($scope,$http){
    var apiUrl="https://localhost:7180/api/productdetail/all";
    $http.get(apiUrl)
    .then(function(res) {
        $scope.productdetails = res.data;
        console.log(res.data);
    })
    .catch(function(err) {
        console.log(err);
        console.error('Error fetching list:', err);
    });

});


app.controller('productdetailDeleteCtrl',function($scope,$http){   
    $scope.deleteproductdetail = function(id){
      $http.get("https://localhost:7180/api/productdetail/delete/"+id).then(function(resp){
          alert("Data Deleted");
      },function(err){
          alert(err);
      });
  }; 
});