
define([
  'jquery',
  'underscore',
  'backbone',
], function ($, _, Backbone) {
    var DelLastAction = function (collection) {
        /// <summary>
        /// Deletes last added element from the collection
        /// </summary>

        this.collection = collection;

    };

    DelLastAction.prototype.execute = function () {
        /// <summary>
        /// Deletes last added rectangle
        /// </summary>
        this.collection.pop();
        
        return true;
    };

    return DelLastAction;
});