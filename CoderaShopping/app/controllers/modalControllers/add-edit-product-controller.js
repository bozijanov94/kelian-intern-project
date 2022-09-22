app.controller("AddEditProductCtrl",
    function ($scope, $uibModalInstance, productService, productId) {

        $scope.product = {
            Id: null,
            Name: "",
            Category: null
        };

        if (productId) {
            $scope.modalState = "Edit product";

            // take the product
            productService.getById(productId)
                .then(function (result) {
                    $scope.product = result.data;
                }, (function (error) {
                    $scope.errorMessages = error.data;
                }))
        }
        else {
            $scope.modalState = "Add product";
        }

        $scope.submit = function (form) {

            $scope.errorMessages = [];

            //check if form is valid
            if (form.$invalid) {
                return;
            }

            // submit the form
            if (productId) {
                //update the product
                productService.updateProduct($scope.product)
                    .then(function (result) {
                        $uibModalInstance.close(result);
                    }, (function (error) {
                        $scope.errorMessages = error.data;
                    }))

            } else {
                //create new product
                productService.createProduct($scope.product)
                    .then(function (result) {
                        if (result.data.Success) {
                            alert(result.data.Message);
                            $uibModalInstance.close(result);
                        } else {
                            if (Array.isArray(result.data.Message)) {
                                $scope.errorMessages = result.data.Message;
                            } else {
                                $scope.errorMessages = [result.data.Message];
                            }
                        }
                        
                    }, (function (error) {
                        $scope.errorMessages = error.data;
                    }))
            }
        }

        $scope.cancel = function () {
            $uibModalInstance.close();
        }
    });