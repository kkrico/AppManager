(function () {
    angular
        .module("appManager.controllers")
        .controller("parseStatusController", ["$scope", "ParseStatusService", ParseStatusController]);

    function ParseStatusController($scope, ParseStatusService) {
        $scope.ParseStatusService = ParseStatusService;
    };
})();