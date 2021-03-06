﻿(function () {

    var ParseStatusService = function (Hub, $rootScope, $log, $timeout) {

        var ParseStatus = this;
        ParseStatus.parsedEntity = null;
        ParseStatus.loading = true;
        var hub = new Hub("parseStatus",
            {
                listeners: {
                    "onEntityParsed": function (entityName) {
                        ParseStatus.parsedEntity = entityName;
                        $log.log(entityName);
                        $rootScope.$apply();
                    }
                }
            });

        return ParseStatus;
    };

    angular
        .module("appManager.services")
        .factory("ParseStatusService",
        ["Hub", "$rootScope", "$log", "$timeout", ParseStatusService]);
})();