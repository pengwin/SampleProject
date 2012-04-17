
define([
  'jquery',
  'underscore',
  'backbone',
  'models/rectanglemodel',
  'actions/resizeaction'
], function ($, _, Backbone, RectangleModel, ResizeAction) {
    var DrawAction = function (collection, modelContructor) {
        /// <summary>
        /// Adds a new model to the collection
        /// </summary>

        this.collection = collection;
        this.modelConstructor = modelContructor;
        this.state = 'ready';
        this.position = { x: 0, y: 0 };

    };

    DrawAction.prototype.setPosition = function (position) {
        if (this.state == 'resizing') {
            this.resizeAction.setPosition(position);
        }
        this.position = position;
    },

    DrawAction.prototype.execute = function () {
        /// <summary>
        /// Creates and adds a new rectangle
        /// </summary>

        if (this.state == 'ready') {
            // add rect
            var rectangle = new this.modelConstructor();
            this.collection.push(rectangle);
            rectangle.set({ x: this.position.x, y: this.position.y, width: 5, height: 5 });
            this.resizeAction = new ResizeAction(rectangle);
            this.state = 'resizing';
            // start resizing
            this.resizeAction.execute();

        } else if (this.state == 'resizing') {
            // end resizing
            this.resizeAction.execute();
            this.state = 'ready';
        }

        return true;
    };

    return DrawAction;
});