app.service("productService", function ($http, SERVER_URL) {

    this.getAll = function () {
        return $http.get(SERVER_URL + "products/getAll");
    } 

    this.createProduct = function (model) {
        return $http({
            method: "post",
            url: SERVER_URL + "products/create",
            data: model
        });
    }

})