var Rectangles = Backbone.Collection.extend({
    model: RectangleModel
});
var Ellipses = Backbone.Collection.extend({
    model: EllipseModel
});

var BlueprintModel = Backbone.Model.extend({

    urlRoot: '/Ajax/Blueprint/',
    defaults: {
        Name: '',
        Description: '',
        JsonData: "{}"
    },
    validate: function (attrs) {
        if ((typeof attrs.Name == 'undefined') || (attrs.Name == '')) {
            return "Name is required";
        }
    },
    initialize: function () {
        this.canvas = new CanvasModel();
        this.rectangles = new Rectangles();
        this.ellipses = new Rectangles();
    },
    parseJsonData: function (jsonData) {
        try {
            var data = JSON.parse(jsonData);
            if (typeof data.canvas != 'undefined') {
                this.canvas.set(data.canvas);
            }
            if (typeof data.rectangles != 'undefined' && data.rectangles.constructor == Array) {
                this.rectangles.reset(data.rectangles);
            }
            if (typeof data.ellipses != 'undefined' && data.ellipses.constructor == Array) {
                this.ellipses.reset(data.ellipses);
            }
        }
        catch (e) {
            console.log("Blueprint JsonData parsing error: " + e);
        }
    },
    parse: function (attrs) {

        if ((typeof attrs.JsonData != 'undefined') && (attrs.JsonData != '')) {
            this.parseJsonData(attrs.JsonData);
        }

        attrs.JsonData = "{}";
        return attrs;
    },
    toJSON: function () {
        var result = Backbone.Model.prototype.toJSON.call(this);

        var jsonData = {};
        var canvas = this.canvas.toJSON();
        jsonData.canvas = canvas;
        var rectangles = this.rectangles.toJSON();
        jsonData.rectangles = rectangles;
        var ellipses = this.ellipses.toJSON();
        jsonData.ellipses = ellipses;

        result.JsonData = JSON.stringify(jsonData);
        return result;
    }
});