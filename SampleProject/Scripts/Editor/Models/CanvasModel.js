var CanvasModel = Backbone.Model.extend({

    defaults: {
        width: 1,
        height: 1,
        gridStep: 1
    },
    initialize: function () {
        this.widthLimit = 1;
        this.heightLimit = 1;
    },
    validate: function (attrs) {

    }
});