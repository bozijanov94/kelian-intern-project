app.controller("productsCtrl",
    function ($scope, productService, $uibModal) {

        $scope.products = [];
        $scope.totalItems = 0;
        $scope.itemsPerPage = 5;
        $scope.currentPage = 1;
        $scope.filter = {
            "Name": null,
            "Category": null
        };
        $scope.orderAscend = true;
        $scope.orderBy = "Name";
        
        $scope.loadProducts = function () {
            var $result = productService.getAll($scope.currentPage, $scope.itemsPerPage, $scope.filter, $scope.orderAscend, $scope.orderBy);
            $result.then(function (result) {
                $scope.products = result.data.Items;
                $scope.totalItems = result.data.TotalItems;
            })
        }

        $scope.loadProducts();

        $scope.removeProduct = function (id) {
            productService.deleteProduct(id)
                .then(function (result) {
                    $scope.loadProducts();
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }

        $scope.addProduct = function () {
            var modalInstance = $uibModal.open({
                templateUrl: './app/templates/modalTemplates/add-edit-product.html',
                controller: 'AddEditProductCtrl',
                resolve: {
                    productId: function () {
                        return null;
                    }
                }
            });

            modalInstance.result
                .then(function (response) {
                    if (response) {
                        //refresh the data in the table
                        $scope.loadProducts();
                    }
                },
                    function (error) {
                        alert("Something bad");
                    });
        }

        $scope.updateProduct = function (id) {
            var modalInstance = $uibModal.open({
                templateUrl: './app/templates/modalTemplates/add-edit-product.html',
                controller: 'AddEditProductCtrl',
                resolve: {
                    productId: function () {
                        return id;
                    }
                }
            });

            modalInstance.result
                .then(function (response) {
                    if (response) {
                        //refresh the data in the table
                        $scope.loadProducts();
                    }
                },
                    function rejection(error) {
                        return error;
                    });
        }

        $scope.descReduc = function (desc) {
            if (desc.length > 40) {
                return desc.substr(0, 40) + "...";
            }
            return desc;
        }

        $scope.descModal = function (desc) {
            var modalInstance = $uibModal.open({
                templateUrl: './app/templates/modalTemplates/description-product.html',
                controller: 'DescriptionProductCtrl',
                resolve: {
                    desc: function () {
                        return desc;
                    }
                }
            });
        }

        $scope.orderChange = function (header) {
            if ($scope.orderBy == header) {
                $scope.orderAscend = !$scope.orderAscend;
            } else {
                $scope.orderAscend = true;
            }
            $scope.orderBy = header;
            $scope.loadProducts();
        }

        $scope.$watch("itemsPerPage", function (newValue, oldValue) {
            if (newValue != oldValue) {
                $scope.loadProducts();
            }
        })

    });