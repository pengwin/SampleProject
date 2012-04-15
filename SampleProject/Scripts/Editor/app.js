
define(['jquery',
        'approuter',
        'models/blueprintmodel',
        'controllers/blueprintcontroller',
        'views/blueprintview'
    ], function ($,AppRouter, BlueprintModel, BlueprintController, BlueprintView) {

        var App = {
            initialize: function () {

                $.ajaxSetup({ headers: { 'apiKey': config.apiKey} });

                this.blueprint = new BlueprintModel();
                this.view = new BlueprintView();
                $("#toolbar").append(this.view.toolbar);
                $("#navbar").append(this.view.navbar);
                $("#infobar").append(this.view.infobar);
                $("#canvas").append(this.view.canvas.paper);
                this.controller = new BlueprintController({ model: this.blueprint, view: this.view });

            },
            
            start: function () {
                this.blueprint.set({ Name: 'Untitled', Description: 'None' });
                this.blueprint.canvas.set({ widthLimit: 500, heightLimit: 500 });
                this.blueprint.canvas.set({ width: 200, height: 200, gridStep: 20, padding: 0 });
                
                var app_router = new AppRouter();
                app_router.blueprint = this.blueprint;
                Backbone.history.start();
            }
        };
        return App;
    });