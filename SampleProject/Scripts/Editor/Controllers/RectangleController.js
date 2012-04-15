// RectangleController module
// controller set the link between rectangle model and rectangle view

define([
        'jquery',
        'underscore',
        'backbone',
        'controllers/basecontroller'
    ], function ($, _, Backbone, BaseController) {

        var RectangelController = BaseController.extend({
            renderForm: function () {
                this.form.render("Rectangle", this.model.toJSON(), ["type"]);
                $('body').append(this.form.el);
            },
            bindHandlers: function () {

                BaseController.prototype.bindHandlers.call(this);

                var self = this;

                this.view.on('edit_request', function () {
                    self.editAction.execute();
                });

            }
        });

        return RectangelController;
    });