// RectnagleView module
// Responsible for displaying rectangle data

define([
        'jquery',
        'underscore',
        'backbone',
        'views/baseview',
        'views/graphics/ellipsegraphics'
    ], function ($, _, Backbone, BaseView,EllipseGraphics) {

        var EllipseView = BaseView.extend({

            initGraphics: function () {
                this.graphics = new EllipseGraphics();
            },
            initNavbar: function () {
                this.navbar = this.make("a", {id: "btnEdit" });
                $(this.navbar).html("Ellipse");
            }
        });

        return EllipseView;
    });