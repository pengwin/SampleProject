// CanvasController module
// controller set the link between canvas model and canvas view

define([
        'jquery',
        'underscore',
        'backbone',
        
        'controllers/basecontroller'
    ], function ($, _, Backbone, BaseController) {

        var CanvasController = BaseController.extend({
            formCaption: "Canvas",
            modelExcludedFields: ["widthLimit","heightLimit","padding"],

            bindHandlers: function () {

                BaseController.prototype.bindHandlers.call(this);

                var self = this;

                this.view.on('navbar_click', function () {
                    self.editAction.execute();
                });

            }
        });

        return CanvasController;
    });