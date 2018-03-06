(function () {

    var iisWebSiteService = function ($http, API) {

        return {
            listarSites: function () {
                return $http.get(API + "/IISWebSite")
                    .then(function (response) {
                        return response.data;
                    });
            }
        }
    };

    angular
        .module("appManager.services")
        .factory("IISWebSite", iisWebSiteService);
})();