app.controller("categoriesCtrl",
    function ($scope, categoryService) {

        $scope.categories = [];
        
        $scope.loadCategories = function () {
            var $result = categoryService.getAll();
            $result.then(function (result) {
                $scope.categories = result.data;
            })
        }

        $scope.loadCategories();

        $scope.category = {
            Name: "Test example"
        };

        $scope.id = {
            Id: "85d715ab-2a6c-4561-b9c5-fd7077f39244"
        };

        $scope.addCategory = function (newCategory) {
            categoryService.createCategory(newCategory)
                .then(function (result) {
                    $scope.newCategoryModel = result.data;
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }

        $scope.removeCategory = function (id) {
            categoryService.deleteCategory(id)
                .then(function (result) {
                    $scope.deletedID = id;
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }
    });