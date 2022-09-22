app.service("orderService", function ($http, SERVER_URL) {

    this.getAll = function (currentPage, itemsPerPage, filter, orderAscend, orderBy) {
        return $http({
            method: "post",
            url: SERVER_URL + "orders/getAll",
            data: { "currentPage": currentPage, "itemsPerPage": itemsPerPage, "filter": filter, "orderAscend": orderAscend, "orderBy": orderBy }
        });
    }

    this.createOrder = function (model) {
        return $http({
            method: "post",
            url: SERVER_URL + "orders/create",
            data: model
        });
    }

    this.deleteOrder = function (id) {
        return $http({
            method: "post",
            url: SERVER_URL + "orders/delete",
            data: { "Id": id }
        });
    }

    this.updateOrder = function (model) {
        return $http({
            method: "post",
            url: SERVER_URL + "orders/update",
            data: model
        });
    }

    this.getById = function (id) {
        return $http({
            method: "post",
            url: SERVER_URL + "orders/getById",
            data: { "Id": id }
        });
    }
})