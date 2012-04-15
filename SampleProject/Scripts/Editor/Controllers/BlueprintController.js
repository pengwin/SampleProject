// BlueprintController module
// controller set the link between blueprint model and blueprint view

define([
        'jquery',
        'underscore',
        'backbone',
        'actions/addrectaction',
        'actions/delrectaction',
        'controllers/basecontroller',
        'controllers/canvascontroller',
        'controllers/rectanglecontroller'
    ], function ($, _, Backbone, AddRectAction, DelRectAction, BaseController, CanvasController, RectangleController) {

        var BlueprintController = BaseController.extend({
            renderForm: function () {
                this.form.render("Blueprint", this.model.toJSON(), ["JsonData"]);
                $('body').append(this.form.el);
            },
            setUp: function () {
                this.canvas = new CanvasController({ model: this.model.canvas, view: this.view.canvas });
            },
            initActions: function () {
                BaseController.prototype.initActions.call(this);
                this.addRectAction = new AddRectAction(this.model.elements);
                this.delRectAction = new DelRectAction(this.model.elements);
            },
            bindHandlers: function () {

                BaseController.prototype.bindHandlers.call(this);

                this.model.elements.on('add', function (model) {
                    if (model.get('type') == 'rectangle') {
                        self.addRectangle(model);
                    }

                });

                this.model.elements.on('remove', function (model) {
                    alert('removed');
                });

                var self = this;

                this.view.on('save_request', function () {
                    self.saveModel();
                });

                this.view.on('load_request', function () {
                    self.loadModel();
                });

                this.view.on('edit_request', function () {
                    self.editAction.execute();
                });

                this.view.on('add_rect_request', function () {
                    self.addRectAction.execute();
                });
                this.view.on('del_rect_request', function () {
                    self.delRectAction.execute();
                });

            },
            addRectangle: function (model) {
                var view = this.view.addRectangle(model.toJSON());
                var controller = new RectangleController({model: model,view: view});
            },
            saveModel: function () {
                this.model.save();
            },
            loadModel: function () {
                this.model.fetch();
            }
        });



        return BlueprintController;
    });