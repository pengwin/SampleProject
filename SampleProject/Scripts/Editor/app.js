
define(['jquery',
        'backbone',
        'models/blueprintmodel',
        'models/rectanglemodel',
        'models/ellipsemodel',
        'controllers/basecontroller',
        'controllers/canvascontroller',
        'controllers/elementcontroller',
        'controllers/elementscontroller',
        'views/blueprintview',
        'views/toolbarview',
        'views/canvasview',
        'views/rectangleview',
        'views/ellipseview',
        'actions/addrectaction',
        'actions/addellipseaction',
        'actions/dellastaction',
        'actions/drawaction'
    ], function ($, Backbone, BlueprintModel, RectangleModel, EllipseModel, // models
                 BaseController, CanvasController, ElementController, ElementsController, //controllers
                 BlueprintView, ToolbarView, CanvasView, RectangleView, EllipseView, // views
                 AddRectAction, AddEllipseAction, DelLastAction, DrawAction) //actions
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
                    modelExcludedFields: ["JsonData","PreviewData"]
                });
                this.canvasController = new CanvasController({ model: this.blueprint.canvas, view: this.canvasView });
                this.elementsController = new ElementsController({
                    elements: this.blueprint.elements,
                    view: this.blueprintView,
                    paper: this.canvasView.graphics.paper(),
                    clickState: this.toolbarView.clickState
                });
            },

            initActions: function () {
                /// <summary>
                /// Initializes actions.
                /// </summary>
                this.addRectAction = new AddRectAction(this.blueprint.elements);
                this.delLastAction = new DelLastAction(this.blueprint.elements);
                this.addEllipseAction = new AddEllipseAction(this.blueprint.elements);
                this.drawRectAction = new DrawAction(this.blueprint.elements, RectangleModel);
                this.drawEllipseAction = new DrawAction(this.blueprint.elements, EllipseModel);
            },

            bindHandlers: function () {
                var self = this;

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
                this.canvasView.on('graphics_click', function () {
                    if (self.toolbarView.clickState.drawRect) {
                        self.drawRectAction.execute();
                    }
                    if (self.toolbarView.clickState.drawEllipse) {
                        self.drawEllipseAction.execute();
                    }
                });
                this.canvasView.on('graphics_mousemove', function (position) {
                    self.drawRectAction.setPosition(position);
                    self.drawEllipseAction.setPosition(position);
                });

                $("#btnSaveNew").click(function () {
                    self.saveNew();
                });
            },

            makePreview: function () {
                /// <summary>
                /// Gets svg from the page 
                /// </summary>

                return $("#canvas").html();
            },

            saveNew: function () {
            	/// <summary>
            	/// Saves the copy of the blueprint.
            	/// </summary>
                this.blueprint.unset('id');
                this.saveModel();
            },

            saveModel: function () {
            	/// <summary>
            	/// Saves the model.
            	/// </summary>
                var self = this;

                var strPreview = this.makePreview();

                this.blueprint.set('PreviewData', strPreview);

                this.blueprint.save({}, {
                    success: function (model, response) {
                        var url = "" + self.blueprint.get('id');
                        self.navigate(url, { trigger: false, replace: true });
                        self.toolbarView.setReadyState();
                    },
                    error: function (model, response) {
                        if (response.responseText == "Unknown API key.") {
                            $("#login_modal").modal('show');
                        } else if (response.responseText == "This model can't be updated with this API key.") {
                            $("#save_new_modal").modal('show');
                        } else {
                            $("#error_modal #error_content").html("Error occurred while model saving: " + response.responseText);
                            $("#error_modal").modal('show');
                        }

                    }
                });
            },

            loadModel: function (id) {
            	/// <summary>
            	/// Fetches model from server.
            	/// </summary>
            	/// <param name="id"></param>
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