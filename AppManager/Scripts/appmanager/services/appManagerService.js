(function() {

    var AppManagerService = function ($http, API) {

        return {
            parse: function () {
                return $http.post(API + "/AppManager/Parse")
                    .then(function (response) {
                        return response.data;
                    });
            }
        }
    };

    angular
        .module("appManager.services")
        .factory("AppManagerService", AppManagerService);

})();