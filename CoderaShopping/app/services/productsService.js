app.service("productService", function ($http, SERVER_URL) {

    this.getAll = function (currentPage, itemsPerPage, filter, orderAscend, orderBy) {
        return $http({
            method: "post",
            url: SERVER_URL + "products/getAll",
            data: { "currentPage": currentPage, "itemsPerPage": itemsPerPage, "filter": filter, "orderAscend": orderAscend, "orderBy": orderBy }
        });
    }

    this.getAllSearch = function (search) {
        return $http({
            method: "post",
            url: SERVER_URL + "products/getAllSearch",
            data: { "Search": search }
        });
    }

    this.createProduct = function (model) {
        return $http({
            method: "post",
            url: SERVER_URL + "products/create",
            data: model
        });
    }

    this.deleteProduct = function (id) {
        return $http({
            method: "post",
            url: SERVER_URL + "products/delete",
            data: { "Id": id }
        });
    }

    this.updateProduct = function (model) {
        return $http({
            method: "post",
            url: SERVER_URL + "products/update",
            data: model
        });
    }

    this.getById = function (id) {
        return $http({
            method: "post",
            url: SERVER_URL + "products/getbyid",
            data: { "Id": id }
        });
    }

})