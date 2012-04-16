
define(['jquery',
        'backbone',
        'models/blueprintmodel',
        'controllers/basecontroller',
        'controllers/canvascontroller',
        'controllers/elementcontroller',
        'views/blueprintview',
        'views/toolbarview',
        'views/canvasview',
        'views/rectangleview',
        'views/ellipseview',
        'actions/addrectaction',
        'actions/addellipseaction',
        'actions/dellastaction'
    ], function ($, Backbone, BlueprintModel,
                 BaseController, CanvasController, ElementController, //controllers
                 BlueprintView, ToolbarView, CanvasView, RectangleView, EllipseView, // views
                 AddRectAction, AddEllipseAction, DelLastAction) //actions
    {

        var AppController = Backbone.Router.extend({
            routes: {
                ":id": "loadModel"
            },
            initialize: function () {
                /// <summary>
                /// Constructor
                /// </summary>

                $.ajaxSetup({ headers: { 'apiKey': config.apiKey} });

                this.initModels();
                this.initViews();
                this.initControllers();
                this.initActions();
                this.bindHandlers();

                this.elementsViews = [];
                this.elementsControllers = [];

                this.start();
            },

            initModels: function () {
                /// <summary>
                /// Initializes models.
                /// </summary>
                this.blueprint = new BlueprintModel();
            },

            initViews: function () {
                /// <summary>
                /// Initializes views.
                /// </summary>
                this.blueprintView = new BlueprintView();
                this.canvasView = new CanvasView();
                this.toolbarView = new ToolbarView();

                $("#toolbar").append(this.toolbarView.el);
                $("#navbar").append(this.blueprintView.navbar);
                $("#infobar").append(this.blueprintView.infobar);
                $("#canvas").append(this.canvasView.graphics.el);

                this.blueprintView.addToGeneral(this.canvasView.navbar);
            },

            initControllers: function () {
                /// <summary>
                /// Initializes controllers.
                /// </summary>
                this.blueprintController = new BaseController({
                    model: this.blueprint,
                    view: this.blueprintView,
                    formCaption: "Blueprint",
                    modelExcludedFields: ["JsonData"]
                });
                this.canvasController = new CanvasController({ model: this.blueprint.canvas, view: this.canvasView });
            },

            initActions: function () {
                /// <summary>
                /// Initializes actions.
                /// </summary>
                this.addRectAction = new AddRectAction(this.blueprint.elements);
                this.delLastAction = new DelLastAction(this.blueprint.elements);
                this.addEllipseAction = new AddEllipseAction(this.blueprint.elements);
            },

            addElement: function (model) {
                /// <summary>
                /// Adds element to current workspace
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
                view.renderGraphics(this.canvasView.graphics.paper());
                view.update(model.toJSON());
                this.elementsViews.push(view);
                view.on('removed', function (sender) {
                    self.elementsViews.pop(sender);
                });
                var controller = new ElementController({ model: model, view: view, formCaption: viewCaption,clickState: this.toolbarView.clickState });
                this.elementsControllers.push(controller);
                controller.on('removed', function (sender) {
                    self.elementsControllers.pop(sender);
                });
            },

            addRectangleView: function () {
                var view = new RectangleView();
                this.blueprintView.addToRectangles(view.navbar);
                return view;
            },
            addEllipseView: function () {
                var view = new EllipseView();
                this.blueprintView.addToEllipses(view.navbar);
                return view;
            },

            bindHandlers: function () {
                var self = this;

                this.blueprint.elements.on('add', function (model) {
                    self.addElement(model);
                });
                this.blueprint.elements.on('remove', function (model) {
                    model.destroy();
                });
                this.blueprint.elements.on('reset', function (collection) {
                    collection.each(function (element) {
                        self.addElement(element);
                    });
                });

                this.toolbarView.on('save_request', function () {
                    self.saveModel();
                });
                this.toolbarView.on('load_request', function () {
                    self.loadModel();
                });
                this.toolbarView.on('add_rect_request', function () {
                    self.addRectAction.execute();
                });
                this.toolbarView.on('add_ellipse_request', function () {
                    self.addEllipseAction.execute();
                });
                this.toolbarView.on('del_last_request', function () {
                    self.delLastAction.execute();
                });
            },

            saveModel: function () {
                var self = this;
                this.blueprint.save({}, {
                    success: function (model, response) {
                        self.toolbarView.setReadyState();
                    },
                    error: function (model, response) {
                        $("#error_modal #error_content").html("Error occurred while model saving: " + response.responseText);
                        $("#error_modal").modal('show');
                    }
                });
            },

            loadModel: function (id) {
                var self = this;
                if (typeof id != 'undefined') {
                    this.blueprint.set('id', id);
                }
                this.blueprint.fetch({
                    success: function (model, response) {
                        self.toolbarView.setReadyState();
                    },
                    error: function (model, response) {
                        $("#error_modal #error_content").html("Error occurred while model loading: " + response.responseText);
                        $("#error_modal").modal('show');
                    }
                });
            },

            start: function () {
                this.blueprint.set({ Name: 'Untitled', Description: 'None' });
                this.blueprint.canvas.set({ widthLimit: 500, heightLimit: 500 });
                this.blueprint.canvas.set({ width: 200, height: 200, gridStep: 20, padding: 0 });

                this.toolbarView.setStartState();

            }
        });
        return AppController;
    });