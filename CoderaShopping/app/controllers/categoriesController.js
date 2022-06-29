app.controller("categoriesCtrl",
    function ($scope, categoryService) {

        $scope.categories = [];

        $scope.loadCategories = function () {
            categoryService.getAll()
                .then(function (result) {
                    $scope.categories = result.data;
                })
        }

        $scope.loadCategories();

        $scope.category = {
            Name: "Test example"
        };

        $scope.addCategory = function (newCategory) {
            categoryService.createCategory(newCategory)
                .then(function (result) {
                    $scope.newCategoryModel = result.data;
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }
    });