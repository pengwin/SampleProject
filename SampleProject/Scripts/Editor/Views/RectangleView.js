// RectnagleView module
// Responsible for displaying rectangle data

define([
        'jquery',
        'underscore',
        'backbone',
        'views/baseview',
        'views/graphics/rectanglegraphics'
    ], function ($, _, Backbone, BaseView, RectangleGraphics) {

        var RectangleView = BaseView.extend({

            initGraphics: function () {
                this.graphics = new RectangleGraphics();
            },
            initNavbar: function () {
                this.navbar = this.make("a", { id: "btnEdit" });
                $(this.navbar).html("Rectangle");
            }
        });

        return RectangleView;
    });