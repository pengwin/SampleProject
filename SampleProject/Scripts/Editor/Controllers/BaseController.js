// BaseController module
// controller set the link between model and view

define([
        'jquery',
        'underscore',
        'backbone',
        'views/formview',
        'actions/editaction',
        'actions/deleteaction'
    ], function ($, _, Backbone, FormView, EditAction, DeleteAction) {

        var BaseController = Backbone.Router.extend({

            // form caption
            formCaption: "",
            // model fields excluded from form view
            modelExcludedFields: [],

            initialize: function (attrs) {
                /// <summary>
                /// Constructor
                /// </summary>
                /// <param name="attrs"></param>
                if (typeof attrs.model == 'undefined') {
                    throw 'Model is not set';
                }
                if (typeof attrs.view == 'undefined') {
                    throw 'View is not set';
                }

                if (typeof attrs.formCaption != 'undefined') {
                    this.formCaption = attrs.formCaption;
                }
                if (typeof attrs.modelExcludedFields != 'undefined') {
                    this.modelExcludedFields = attrs.modelExcludedFields;
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
                /// <summary>
                /// Sets up some aditional parameters
                /// </summary>


            },
            bindHandlers: function () {
                /// <summary>
                /// Binds handlers
                /// </summary>

                var self = this;

                this.model.on('change', function () {
                    self.updateView();
                });

                this.model.on('destroy', function () {
                    self.remove();
                });

                this.view.on('infobar_click', function () {
                    self.editAction.execute();
                });
                /*
                this.view.on('graphics_click', function () {
                self.editAction.execute();
                });*/
            },

            initActions: function () {
                /// <summary>
                /// Init main actions
                /// </summary>
                this.editAction = new EditAction(this.form, this.model);
            },
            renderForm: function () {
                /// <summary>
                /// Renders form
                /// </summary>
                this.form.render(this.formCaption, this.model.toJSON(), this.modelExcludedFields);
                $('body').append(this.form.el);
            },

            updateView: function () {
                /// <summary>
                /// Updates view
                /// </summary>
                var data = this.model.toJSON();
                this.view.update(data);
            },
            remove: function () {
                this.form.remove();
                this.view.remove();
                this.trigger('removed', this);
            }
        });

        return BaseController;
    });