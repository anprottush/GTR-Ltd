var app = angular.module('my-app', []);
app.controller('detailCtrl',($scope,$http,$window)=>{
  $scope.productdetail = ()=>{
    var data = 
    {
        PurchaseCustomer:$scope.PurchaseCustomer, 
        PurchaseDate:$scope.PurchaseDate,
        SalesEmployee:$scope.SalesEmployee, 
        Description:$scope.Description,
        Stock:$scope.Stock, 
        Status:$scope.Status,
        ProductMasterId:$scope.ProductMasterId, 
    };
    $http.post("https://localhost:7180/api/productdetail/add",data).then(function(res){
            alert("Product created successfully");
            $window.location.href = 'productdetailList.html';
            
        }).catch(function(err){
            //debugger;
            alert("Created failed incorrect information");
            //console.error('Error saving data:', err);
        });
   
  };
});
