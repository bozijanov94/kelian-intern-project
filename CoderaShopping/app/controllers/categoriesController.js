app.controller("categoriesCtrl",
    function ($scope, categoryService, $uibModal) {

        $scope.categories = [];

        $scope.loadCategories = function () {
            var $result = categoryService.getAll();
            $result.then(function (result) {
                $scope.categories = result.data;
            })
        }

        $scope.loadCategories();

        $scope.removeCategory = function (id) {
            categoryService.deleteCategory(id)
                .then(function (result) {
                    $scope.deletedID = id;
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
                    }
                },
                    function rejection(error) {
                        return error;
                    });
        }


    });