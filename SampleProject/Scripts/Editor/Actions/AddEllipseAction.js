
define([
  'jquery',
  'underscore',
  'backbone',
  'models/ellipsemodel'
], function ($, _, Backbone, EllipseModel) {
    var AddEllipseAction = function (collection) {
        /// <summary>
        /// Adds a new ellipse model to the collection
        /// </summary>

        this.collection = collection;

    };

    AddEllipseAction.prototype.execute = function () {
        /// <summary>
        /// Creates and adds a new ellipse
        /// </summary>
        var ellipse = new EllipseModel();
        this.collection.push(ellipse);
        ellipse.set({ x: 0, y: 0, width: 5, height: 5 });
        
        return true;
    };

    return AddEllipseAction;
});