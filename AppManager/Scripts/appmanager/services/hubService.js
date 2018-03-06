﻿(function (jquery) {

    angular
        .module("appManager.services")
        .constant("$", jquery)
        .factory("Hub", ["$", function ($) {


            var globalConnection = null;

            var initGlobalConnection = function (options) {
                if (options && options.rootPath) {
                    globalConnection = $.hubConnection(options.rootPath, {
                        userDefaultPath: false
                    });
                } else {
                    globalConnection = $.hubConnection();
                }
            };

            return function (hubName, options) {
                var Hub = this;

                if (globalConnection === null) {
                    initGlobalConnection(options);
                }
                Hub.connection = globalConnection;
                Hub.proxy = Hub.connection.createHubProxy(hubName);

                Hub.on = function (event, fn) {
                    Hub.proxy.on(event, fn);
                };
                Hub.invoke = function (method, args) {
                    return Hub.proxy.invoke.apply(Hub.proxy, arguments);
                };
                Hub.disconnect = function () {
                    Hub.connection.stop();
                };
                Hub.connect = function () {
                    Hub.connection.start();
                };

                if (options && options.listeners) {
                    angular.forEach(options.listeners, function (fn, event) {
                        Hub.on(event, fn);
                    });
                }
                if (options && options.methods) {
                    angular.forEach(options.methods, function (method) {
                        Hub[method] = function () {
                            var args = $.makeArray(arguments);
                            args.unshift(method);
                            return Hub.invoke.apply(Hub, args);
                        };
                    });
                }
                if (options && options.queryParams) {
                    Hub.connection.qs = options.queryParams;
                }
                if (options && options.errorHandler) {
                    Hub.connection.error(options.errorHandler);
                }

                Hub.promise = Hub.connection.start();
                return Hub;
            };

        }]);
})(window.jQuery);