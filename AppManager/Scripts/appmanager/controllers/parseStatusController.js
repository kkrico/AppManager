(function () {
    angular
        .module("appManager.controllers")
        .controller("parseStatusController", ["$scope", "ParseStatusService" , "AppManagerService", "$window", "URLRoot", ParseStatusController]);

    function ParseStatusController($scope, parseStatusService, appManagerService, $window, URLRoot) {
        $scope.ParseStatusService = parseStatusService;

        appManagerService.parse().then(function(hasSucesso) {

            if (hasSucesso) {
                $window.location.href = URLRoot + "/Home";
            }
            debugger;
        });
    };
})();