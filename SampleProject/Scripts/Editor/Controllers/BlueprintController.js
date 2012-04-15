// BlueprintController module
// controller set the link between blueprint model and blueprint view

define([
        'jquery',
        'underscore',
        'backbone',
        'controllers/basecontroller',
        'controllers/canvascontroller'
    ], function ($, _, Backbone, BaseController, CanvasController) {

        var BlueprintController = BaseController.extend({
            renderForm: function () {
                this.form.render("Blueprint", this.model.toJSON(), ["JsonData"]);
                $('body').append(this.form.el);
            },
            setUp: function () {
                this.canvas = new CanvasController({ model: this.model.canvas, view: this.view.canvas });
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



        return BlueprintController;
    });