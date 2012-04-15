// BaseController module
// controller set the link between model and view

define([
        'jquery',
        'underscore',
        'backbone',
        'views/formview',
        'actions/editaction'
    ], function ($, _, Backbone, FormView, EditAction) {

        var BaseController = Backbone.Router.extend({

            initialize: function (attrs) {

                if (typeof attrs.model == 'undefined') {
                    throw 'Model is not set';
                }
                if (typeof attrs.view == 'undefined') {
                    throw 'View is not set';
                }

                this.model = attrs.model;
                this.view = attrs.view;

                this.form = new FormView();
                this.renderForm();

                this.initActions();
                this.bindHandlers();

                this.setUp();
            },

            setUp: function () {

            },
            bindHandlers: function () {

                var self = this;

                this.model.on('change', function () {
                    self.updateView();
                });
            },
            initActions: function () {
                this.editAction = new EditAction(this.form, this.model);
            },
            renderForm: function () {

            },

            updateView: function () {
                var data = this.model.toJSON();
                this.view.update(data);
            }
        });

        return BaseController;
    });