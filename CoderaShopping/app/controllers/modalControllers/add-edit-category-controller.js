app.controller("AddEditCategoryCtrl",
    function ($scope, $uibModalInstance, categoryService, categoryId) {

        $scope.category = {
            Id: null,
            Name: ""
        };

        if (categoryId) {
            $scope.modalState = "Edit category";

            // take the category
            categoryService.getById(categoryId)
                .then(function (result) {
                    $scope.category = result.data;
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }
        else {
            $scope.modalState = "Add category";
        }       

        $scope.submit = function (form) {

            //check if form is valid
            if (form.$invalid) {
                return;
            }

            // submit the form
            if (categoryId) {
                //update the category
                categoryService.updateCategory($scope.category)
                    .then(function (result) {
                        $uibModalInstance.close(response);
                    }, (function (error) {
                        $scope.errorMessage = error.data;
                    }))

            } else {
                //create new category
                categoryService.createCategory($scope.category)
                    .then(function (result) {
                        $uibModalInstance.close(response);
                    }, (function (error) {
                        $scope.errorMessage = error.data;
                    }))
            }
        }

        $scope.cancel = function () {
            $uibModalInstance.close();
        }
    });