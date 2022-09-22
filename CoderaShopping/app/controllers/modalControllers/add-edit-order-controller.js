app.controller("AddEditOrderCtrl",
    function ($scope, $uibModalInstance, orderService, orderId) {

        $scope.order = {
            Id: null,
            Customer: "",
            Product: "",
            Quantity: ""
        };

        if (orderId) {
            $scope.modalState = "Edit order";

            // take the category
            orderService.getById(orderId)
                .then(function (result) {
                    $scope.order = result.data;
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }
        else {
            $scope.modalState = "Add order";
        }

        $scope.submit = function (form) {

            //check if form is valid
            if (form.$invalid) {
                return;
            }

            // submit the form
            if (orderId) {
                //update the category
                orderService.updateOrder($scope.order)
                    .then(function (result) {
                        $uibModalInstance.close(result);
                    }, (function (error) {
                        $scope.errorMessage = error.data;
                    }))

            } else {
                //create new category
                orderService.createOrder($scope.order)
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