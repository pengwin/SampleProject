// Module: DeleteAction

define([
  'jquery',
  'underscore',
  'backbone'
], function ($, _, Backbone) {
    var DeleteAction = function (model) {
        /// <summary>
        /// Destroys the  model
        /// </summary>

        this.model = model;

    };

    DeleteAction.prototype.execute = function () {
        /// <summary>
        /// Destroys the model
        /// </summary>

        this.model.destroy();

        return true;
    };

    return DeleteAction;
});