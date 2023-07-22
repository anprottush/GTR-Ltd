var app = angular.module('my-app', []);
app.controller('loginCtrl',($scope,$http,$window)=>{
    $scope.user = {
        username: '',
        password: ''
      };
      $scope.errmsg = '';
  $scope.validLogin = ()=>{
    if ($scope.user.username && $scope.user.password) {
        var apiUrl = 'https://localhost:7180/api/user/login';
        var data = 
        {
            Username: $scope.user.username,
            Password: $scope.user.password
        };
        $http.post(apiUrl,data).then(function(res){
            var token = res.data.token;
            var time= res.data.time;
            localStorage.setItem("Token",token);
            localStorage.setItem("Time",time);
            localStorage.getItem("Token");
            alert("login Successful");
            $window.location.href = 'dashboard.html';
            //logout
            //localStorage.removeItem("_token");
            //debugger;
        }).catch(function(err){
            if (err.status === 400 && err.data && err.data.errors) {
                //$scope.errmsg = 'Validation errors occurred:';
                alert("login failed incorrect information");
                angular.forEach(err.data.errors, function(errors, field) {
                    $scope.errmsg += '\n' + field + ': ' + errors.join(', ');
                });
            }
            else{
                $scope.errmsg = 'An error occurred during login. Please Create Account First and try to login.';
            }

            //debugger;
            
            //console.error('Error saving data:', err);
        });
    }
    else{
        $scope.errmsg = 'Please fill in all required fields.';
    }
   
  };
});



































/*

 $http.post("https://localhost:7041/api/user/add",data).then(function(resp){
            //localStorage.setItem("_token","ABCD");
            //localStorage.getItem("_token");
            alert("Data Insert Successfull");
            //logout
            //localStorage.removeItem("_token");
            //debugger;
        },function(err){
            //debugger;
            alert(err);
        })
























function validForm(data)
{
    let error = document.getElementById("errmsg");
    let username=data.UserName.value;
    let password=data.Password.value;
    if(username=="")
    {
        error.innerHTML="Username is required";
        return false;
    }
    if(password=="")
    {
        error.innerHTML="Password is required";
        return false;
    }
    else
    {
        if(username=="p" && password=="1")
        {
            //document.location.href="index.html";
                //parent.open("index.html");// new window tab
                //window.open("index.html");// new window tab
                //location.replace("https://www.w3schools.com");
                
                 //location.href="https://www.aiub.edu";
                window.location.href="https://www.aiub.edu";
               
            //alert("Thank You for Login");

            //window.location="index.html";
            return true;
        }
        else
        {
            error.innerHTML="can not login";
            return false;
        }  
    }

  
}*/
