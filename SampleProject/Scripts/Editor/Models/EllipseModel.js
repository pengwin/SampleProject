var EllipseModel = Backbone.Model.extend({

    defaults: {
        x: 0,
        y: 0,
        horizontalRadius: 1,
        verticalRadius: 1
    },
    initialize: function () {
        this.topLimit = 1;
        this.bottomLimit = 1;
        this.leftLimit = 1;
        this.rightLimit = 1;
    },
    validate: function (attrs) {

    }
});