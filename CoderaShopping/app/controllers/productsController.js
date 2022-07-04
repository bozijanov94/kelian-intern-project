app.controller("productsCtrl",
    function ($scope, productService) {

        $scope.products = [];
        
        $scope.loadProducts = function () {
            var $result = productService.getAll();
            $result.then(function (result) {
                $scope.products = result.data;
            })
        }

        $scope.loadProducts();

        $scope.product = {
            Name: "Test example"
        };

        $scope.addProduct = function (newProduct) {
            productService.createProduct(newProduct)
                .then(function (result) {
                    $scope.newProductModel = result.data;
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }
    });