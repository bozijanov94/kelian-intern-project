app.controller("ordersCtrl",
    function ($scope, orderService, $uibModal) {

        $scope.orders = [];
        $scope.totalItems = 0;
        $scope.itemsPerPage = 5;
        $scope.currentPage = 1;
        $scope.filter = {
            "Customer": null,
            "Product": null,
        };
        $scope.orderAscend = true;
        $scope.orderBy = "DateOrdered";

        $scope.loadOrders = function () {
            var $result = orderService.getAll($scope.currentPage, $scope.itemsPerPage, $scope.filter, $scope.orderAscend, $scope.orderBy);
            $result.then(function (result) {
                $scope.orders = result.data.Items;
                $scope.totalItems = result.data.TotalItems;
            })
        }

        $scope.loadOrders();

        $scope.removeOrder = function (id) {
            orderService.deleteOrder(id)
                .then(function (result) {
                    $scope.loadOrders();
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }

        $scope.addOrder = function () {
            var modalInstance = $uibModal.open({
                templateUrl: './app/templates/modalTemplates/add-edit-order.html',
                controller: 'AddEditOrderCtrl',
                resolve: {
                    orderId: function () {
                        return null;
                    }
                }
            });

            modalInstance.result
                .then(function (response) {
                    if (response) {
                        //refresh the data in the table
                        $scope.loadOrders();
                    }
                },
                    function rejection(error) {
                        return error;
                    });
        }

        $scope.updateOrder = function (id) {
            var modalInstance = $uibModal.open({
                templateUrl: './app/templates/modalTemplates/add-edit-order.html',
                controller: 'AddEditOrderCtrl',
                resolve: {
                    orderId: function () {
                        return id;
                    }
                }
            });

            modalInstance.result
                .then(function (response) {
                    if (response) {
                        //refresh the data in the table
                        $scope.loadOrders();
                    }
                },
                    function rejection(error) {
                        return error;
                    });
        }

        $scope.orderChange = function (header) {
            if ($scope.orderBy == header) {
                $scope.orderAscend = !$scope.orderAscend;
            } else {
                $scope.orderAscend = true;
            }
            $scope.orderBy = header;
            $scope.loadUsers();
        }

        $scope.$watch("itemsPerPage", function (newValue, oldValue) {
            if (newValue != oldValue) {
                $scope.loadOrders();
            }
        })

    });