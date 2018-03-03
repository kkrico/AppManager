(function () {

    angular
        .module("appManager.services")
        .factory("Site", function ($http, API) {

            return {
                listarSites: function () {
                    return $http.get(API + "/site")
                        .then(function (response) {
                            return response.data;
                        });
                }
            }
        });
})();