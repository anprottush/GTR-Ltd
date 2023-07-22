var app = angular.module('my-app', []);
app.controller('registrationCtrl',($scope,$http)=>{
    $scope.user = {
        username: '',
        password: ''
      };
      $scope.errmsg = '';
  $scope.validRegistration = ()=>{
    if ($scope.user.username && $scope.user.password) {
        var apiUrl = 'https://localhost:7180/api/User/add';
        var data = 
        {
            Username: $scope.user.username,
            Password: $scope.user.password
        };
        $http.post(apiUrl,data).then(function(res){
            alert("User Registration Successfull");
            
        }).catch(function(err){
            if (err.status === 400 && err.data && err.data.errors) {
                alert("Validation errors occurred");
            }
            else{
                $scope.errmsg = 'An error occurred';
            }

        });
    }
    else{
        $scope.errmsg = 'Please fill in all required fields.';
    }
   
  };
});
































/*
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
