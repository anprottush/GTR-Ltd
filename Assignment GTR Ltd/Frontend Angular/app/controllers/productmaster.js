var app = angular.module('my-app', []);
app.controller('masterCtrl',($scope,$http,$window)=>{
  $scope.productmasteradd = ()=>{
    var data = 
    {
        Name:$scope.Name, 
        Barcode:$scope.Barcode,
        Type:$scope.Type, 
        Quantity:$scope.Quantity,
        Price:$scope.Price, 
    };
    $http.post("https://localhost:7180/api/productmaster/add",data).then(function(res){
            alert("Product created successfully");
            $window.location.href = 'productmasterList.html';
        }).catch(function(err){
            alert("Fields are required");
            
        });
  };
});
