﻿// CanvasController module
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

                this.view.on('edit_request', function () {
                    self.editAction.execute();
                });

            }
        });

        return CanvasController;
    });