// Use this as a quick template for future modules
define([
  'jquery',
  'underscore',
  'backbone'
], function ($, _, Backbone) {
    var EditAction = function (formView, model) {
        /// <summary>
        /// Links form and model
        /// handles form button click events and model change event
        /// </summary>

        this.formView = formView;
        this.model = model;

        var self = this;

        // form submit ok handler
        this.formView.submitButton.click(function () {
            var data = self.formView.fetch();
            var error = self.model.validate(data);
            if (error) {
                self.formView.message('Error: ' + error);
            }
            else {
                self.model.set(data);
                self.formView.hide();
            }
        });

        // form submit cancel handler
        this.formView.cancelButton.click(function () {
            self.formView.hide();
        });

    };

    EditAction.prototype.execute = function () {
        /// <summary>
        /// Shows linked form
        /// </summary>

        this.formView.message('');
        var data = this.model.toJSON();
        this.formView.update(data);
        this.formView.show();

        return true;
    };

    return EditAction;
});