// ElementsController module
// controller sets the link between elements collection and view
// instantiates elements views and controllers

define([
        'jquery',
        'underscore',
        'backbone',
        'views/rectangleview',
        'views/ellipseview',
        'controllers/elementcontroller'
    ], function ($, _, Backbone, RectangleView, EllipseView, ElementController) {

        var ElementsController = Backbone.Router.extend({

            initialize: function (attrs) {
                /// <summary>
                /// Constructor.
                /// </summary>
                /// <param name="attrs"></param>
                if (typeof attrs.elements == 'undefined') {
                    throw "elements attribute is undefined";
                }
                if (typeof attrs.view == 'undefined') {
                    throw "view attribute is undefined";
                }
                if (typeof attrs.paper == 'undefined') {
                    throw "paper attribute is undefined";
                }
                if (typeof attrs.clickState == 'undefined') {
                    throw "clickState attribute is undefined";
                }

                this.elements = attrs.elements;
                this.view = attrs.view;
                this.paper = attrs.paper;
                this.clickState = attrs.clickState;

                this.elementsViews = [];
                this.elementsControllers = [];

                this.bindHandlers();
            },

            addElement: function (model) {
                /// <summary>
                /// Adds element to current workspace
                /// Instantiates view and controller
                /// </summary>
                /// <param name="model"></param>
                var view;
                var viewCaption = "";
                // get view
                if (model.get('type') == 'rectangle') {
                    view = this.addRectangleView();
                    viewCaption = "Rectangle";
                }
                if (model.get('type') == 'ellipse') {
                    view = this.addEllipseView(model);
                    viewCaption = "Ellipse";
                }
                // if no match exit func
                if (typeof view == 'undefined') return;
                var self = this;
                view.renderGraphics(this.paper);
                view.update(model.toJSON());
                this.elementsViews.push(view);
                view.on('removed', function (sender) {
                    self.elementsViews.pop(sender);
                });
                var controller = new ElementController({ model: model, view: view, formCaption: viewCaption, clickState: this.clickState });
                this.elementsControllers.push(controller);
                controller.on('removed', function (sender) {
                    self.elementsControllers.pop(sender);
                });
            },

            addRectangleView: function () {
                /// <summary>
                /// Instantiates rectangle view
                /// </summary>
                /// <returns type="">created view</returns>
                var view = new RectangleView();
                this.view.addToRectangles(view.navbar);
                return view;
            },

            addEllipseView: function () {
                /// <summary>
                /// Instantiates ellipse view
                /// </summary>
                /// <returns type=""></returns>
                var view = new EllipseView();
                this.view.addToEllipses(view.navbar);
                return view;
            },

            bindHandlers: function () {
                /// <summary>
                /// Binds handlers to collection
                /// </summary>

                var self = this;

                this.elements.on('add', function (model) {
                    self.addElement(model);
                });
                this.elements.on('remove', function (model) {
                    model.destroy();
                });
                this.elements.on('reset', function (collection) {
                    collection.each(function (element) {
                        self.addElement(element);
                    });
                });
            }
        });

        return ElementsController;
    });