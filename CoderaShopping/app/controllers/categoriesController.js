app.controller("categoriesCtrl",
    function ($scope, categoryService) {

        $scope.categories = [];

        $scope.loadCategories = function () {
            categoryService.getAll().then(function (result) {
                $scope.categories = result.data;
            })
        }

        $scope.loadCategories();
    });