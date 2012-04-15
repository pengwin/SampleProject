define([
        'jquery',
        'underscore',
        'backbone',
    ], function ($, _, Backbone) {
        
        var AppRouter = Backbone.Router.extend({
            routes: {
                ":id": "setBlueprintId"
            },
            setBlueprintId: function (id) {
                if (!isNaN(id)) {
                        this.blueprint.set('id', id);
                        this.blueprint.fetch();
                }
            }
        });
        return AppRouter;
    });
