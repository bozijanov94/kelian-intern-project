app.service("categoryService", function ($http, SERVER_URL) {

    this.getAll = function (currentPage, itemsPerPage, filter, orderAscend, orderBy) {
        return $http({
            method: "post",
            url: SERVER_URL + "categories/getAll",
            data: { "currentPage": currentPage, "itemsPerPage": itemsPerPage, "filter": filter, "orderAscend": orderAscend, "orderBy": orderBy }
        });
    }

    this.getAllAvailable = function (search, status) {
        return $http({
            method: "post",
            url: SERVER_URL + "categories/getAllAvailable",
            data: { "Search": search, "Status": status }
        });
    }

    this.createCategory = function (model) {
        return $http({
            method: "post",
            url: SERVER_URL + "categories/create",
            data: model
        });
    }

    this.deleteCategory = function (id) {
        return $http({
            method: "post",
            url: SERVER_URL + "categories/delete",
            data: { "Id": id }
        });
    }

    this.updateCategory = function (model) {
        return $http({
            method: "post",
            url: SERVER_URL + "categories/update",
            data: model
        });
    }

    this.getById = function (id) {
        return $http({
            method: "post",
            url: SERVER_URL + "categories/getById",
            data: { "Id": id }
        });
    }
})