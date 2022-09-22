app.service("userService", function ($http, SERVER_URL) {

    this.getAll = function (currentPage, itemsPerPage, filter, orderAscend, orderBy) {
        return $http({
            method: "post",
            url: SERVER_URL + "users/getAll",
            data: { "currentPage": currentPage, "itemsPerPage": itemsPerPage, "filter": filter, "orderAscend": orderAscend, "orderBy": orderBy }
        });
    }

    this.getAllSearch = function (search) {
        return $http({
            method: "post",
            url: SERVER_URL + "users/getAllSearch",
            data: { "Search": search }
        });
    }

    this.createUser = function (model) {
        return $http({
            method: "post",
            url: SERVER_URL + "users/create",
            data: model
        });
    }

    this.deleteUser = function (id) {
        return $http({
            method: "post",
            url: SERVER_URL + "users/delete",
            data: { "Id": id }
        });
    }

    this.updateUser = function (model) {
        return $http({
            method: "post",
            url: SERVER_URL + "users/update",
            data: model
        });
    }

    this.getById = function (id) {
        return $http({
            method: "post",
            url: SERVER_URL + "users/getById",
            data: { "Id": id }
        });
    }
})