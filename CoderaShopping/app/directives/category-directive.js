app.directive("categoryDirective",
    function (categoryService) {
        return {
            templateUrl: "/app/directives/category.html",
            restrict: "E",
            scope: {
                model: "=",
                status: "=",
                preselectDefault: "="
            },
            link: function ($scope, $element, $attributes) {
                $scope.Categories = [];

                $scope.fetchData = function (search) {
                    var $result = categoryService.getAllAvailable(search, $scope.status);
                    $result.then(function (result) {
                        if ($scope.preselectDefault && !$scope.Categories.length && result.data.length) {
                            var defaultCat = result.data.filter(function (item) {
                                if (item.IsDefault) {
                                    return item;
                                }
                            });
                            if (defaultCat && defaultCat.length) {
                                $scope.model = defaultCat[0];
                            }
                        }
                        $scope.Categories = result.data;
                    })
                }

                $scope.fetchData("");

                $scope.change = function (item) {
                    $scope.model = item;
                }
            }
        }
    });

