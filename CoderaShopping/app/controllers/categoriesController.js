app.controller("categoriesCtrl",
    function ($scope, categoryService, $uibModal) {

        $scope.categories = [];
        $scope.totalItems = 0;
        $scope.itemsPerPage = 5;
        $scope.currentPage = 1;
        $scope.filter = {
            "Name": null,
            "Status": null,
            "IsDefault": null
        };
        $scope.orderAscend = true;
        $scope.orderBy = "IsDefault";

        $scope.loadCategories = function () {
            var $result = categoryService.getAll($scope.currentPage, $scope.itemsPerPage, $scope.filter, $scope.orderAscend, $scope.orderBy);
            $result.then(function (result) {
                $scope.categories = result.data.Items;
                $scope.totalItems = result.data.TotalItems;
            })
        }

        $scope.loadCategories();

        $scope.removeCategory = function (id) {
            categoryService.deleteCategory(id)
                .then(function (result) {
                    $scope.loadCategories();
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }

        $scope.addCategory = function () {
            var modalInstance = $uibModal.open({
                templateUrl: './app/templates/modalTemplates/add-edit-category.html',
                controller: 'AddEditCategoryCtrl',
                resolve: {
                    categoryId: function () {
                        return null;
                    }
                }
            });

            modalInstance.result
                .then(function (response) {
                    if (response) {
                        //refresh the data in the table
                        $scope.loadCategories();
                    }
                },
                    function rejection(error) {
                        return error;
                    });
        }

        $scope.updateCategory = function (id) {
            var modalInstance = $uibModal.open({
                templateUrl: './app/templates/modalTemplates/add-edit-category.html',
                controller: 'AddEditCategoryCtrl',
                resolve: {
                    categoryId: function () {
                        return id;
                    }
                }
            });

            modalInstance.result
                .then(function (response) {
                    if (response) {
                        //refresh the data in the table
                        $scope.loadCategories();
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
            $scope.loadCategories();
        }

        $scope.$watch("itemsPerPage", function (newValue, oldValue) {
            if (newValue != oldValue) {
                $scope.loadCategories();
            }
        })

    });