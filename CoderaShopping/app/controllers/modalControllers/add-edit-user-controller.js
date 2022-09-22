app.controller("AddEditUserCtrl",
    function ($scope, $uibModalInstance, userService, userId) {

        $scope.user = {
            Id: null,
            Name: ""
        };

        if (userId) {
            $scope.modalState = "Edit user";

            // take the category
            userService.getById(userId)
                .then(function (result) {
                    $scope.user = result.data;
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }
        else {
            $scope.modalState = "Add user";
        }

        $scope.submit = function (form) {

            //check if form is valid
            if (form.$invalid) {
                return;
            }

            // submit the form
            if (userId) {
                //update the category
                userService.updateUser($scope.user)
                    .then(function (result) {
                        $uibModalInstance.close(result);
                    }, (function (error) {
                        $scope.errorMessage = error.data;
                    }))

            } else {
                //create new category
                userService.createUser($scope.user)
                    .then(function (result) {
                        $uibModalInstance.close(result);
                    }, (function (error) {
                        $scope.errorMessage = error.data;
                    }))
            }
        }

        $scope.cancel = function () {
            $uibModalInstance.close();
        }
    });