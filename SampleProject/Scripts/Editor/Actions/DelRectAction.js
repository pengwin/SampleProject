
define([
  'jquery',
  'underscore',
  'backbone',
], function ($, _, Backbone) {
    var DelRectAction = function (collection) {
        /// <summary>
        /// Deletes last added rectangle model from the collection inside model
        /// </summary>

        this.collection = collection;

    };

    DelRectAction.prototype.execute = function () {
        /// <summary>
        /// Deletes last added rectangle
        /// </summary>
        this.collection.pop();
        
        return true;
    };

    return DelRectAction;
});