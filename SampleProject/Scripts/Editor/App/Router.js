var AppRouter = Backbone.Router.extend({
    routes: {
        ":id": "setBlueprintId",
        ":id/canvas": "editCanvas",
        "canvas": "editCanvas",
        "*actions": "defaultRoute" // Backbone will try match the route above first
    },
    setBlueprintId: function (id) {
        if (!isNaN(id)) {
            this.blueprint.set('id', id);
            this.blueprint.fetch();
        }
    },
    editCanvas: function (id) {
        if (typeof id != 'undefined') {
            this.setBlueprintId(id);
        }
        this.blueprint.canvas.set('width', 300);
        alert(this.blueprint.canvas.get('width'));
    },
    defaultRoute: function (actions) {
        //alert(actions);
    }
});
