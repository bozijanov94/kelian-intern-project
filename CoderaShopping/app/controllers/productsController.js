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
            Name: "Google Pixel",
            Description: "It is a phone by Google",
            Category: {
                Id: "2f490780-61b1-40e6-9ea9-aec9013c2338",
                Name: "Phone"
            }
        };

        $scope.addProduct = function (newProduct) {
            productService.createProduct(newProduct)
                .then(function (result) {
                    $scope.newProductModel = result.data;
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }

        $scope.removeProduct = function (id) {
            productService.deleteProduct(id)
                .then(function (result) {
                    $scope.deletedID = id;
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }

        $scope.alterProduct = function (changedProduct) {
            productService.updateProduct(changedProduct)
                .then(function (result) {
                    $scope.changedProductModel = result.data;
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }
    });