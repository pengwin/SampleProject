
define([
  'jquery',
  'underscore',
  'backbone',
  'models/rectanglemodel'
], function ($, _, Backbone, RectangleModel) {
    var AddRectAction = function (collection) {
        /// <summary>
        /// Adds a new rectangle model to the collection inside model
        /// </summary>

        this.collection = collection;

    };

    AddRectAction.prototype.execute = function () {
        /// <summary>
        /// Creates and adds a new rectangle
        /// </summary>
        var rectangle = new RectangleModel();
        this.collection.push(rectangle);
        rectangle.set({ x: 0, y: 0, width: 5, height: 5 });
        return true;
    };

    return AddRectAction;
});