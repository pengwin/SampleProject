// CanvasController module
// controller set the link between canvas model and canvas view

define([
        'jquery',
        'underscore',
        'backbone',
        'controllers/basecontroller'
    ], function ($, _, Backbone, BaseController) {

        var CanvasController = BaseController.extend({
            renderForm: function () {
                this.form.render("Canvas", this.model.toJSON(), ["widthLimit","heightLimit","padding"]);
                $('body').append(this.form.el);
            },
            bindHandlers: function () {

                BaseController.prototype.bindHandlers.call(this);

                var self = this;

                this.view.on('save_request', function () {
                    self.saveModel();
                });

                this.view.on('edit_request', function () {
                    self.editAction.execute();
                });

            },
            saveModel: function () {
                this.model.save();
                if (!this.model.isNew()) {
                    this.view.setReadyState();
                }
            }
        });

        return CanvasController;
    });