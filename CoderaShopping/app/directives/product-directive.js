app.directive("productDirective",
    function (productService) {
        return {
            templateUrl: "/app/directives/product.html",
            restrict: "E",
            scope: {
                model: "="
            },
            link: function ($scope, $element, $attributes) {
                $scope.Products = [];

                $scope.fetchData = function (search) {
                    var $result = productService.getAllSearch(search);
                    $result.then(function (result) {
                        $scope.Products = result.data;
                    })
                }

                $scope.fetchData("");

                $scope.change = function (item) {
                    $scope.model = item;
                }
            }
        }
    });

