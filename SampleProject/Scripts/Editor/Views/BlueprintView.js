// BlueprintView module
// Responsible for displaying blueprint data

define([
        'jquery',
        'underscore',
        'backbone',
        'views/baseview',
        'text!templates/navbar.htm',
        'text!templates/infobar.htm'
    ], function ($, _, Backbone, BaseView, navbarTemplate, infobarTemplate) {

        var BlueprintView = BaseView.extend({

            initInfobar: function () {
                this.infobar = this.make("div", { class: "row" });
                var context = {};
                var compiledTemplate = _.template(infobarTemplate, context);
                $(this.infobar).html(compiledTemplate);
            },
            initNavbar: function () {
                this.navbar = this.make("div", { class: "sidebar-nav" });
                var context = {};
                var compiledTemplate = _.template(navbarTemplate, context);
                $(this.navbar).html(compiledTemplate);
            },
            addToGeneral: function (el) {
                $("#general_nav", this.navbar).append(el);
            },
            addToRectangles: function (el) {
                $("#rect_nav", this.navbar).append(el);
            },
            addToEllipses: function (el) {
                $("#ellipse_nav", this.navbar).append(el);
            },
            _bindInfobar: function () {
                /// <summary>
                /// Binds handlers to infobar
                /// </summary>

                var self = this;

                if (typeof this.infobar == 'undefined') return;
                $('#btnBlueprintEdit', this.infobar).click(function () {
                    self.trigger('infobar_click');
                });
            },
            updateInfobar: function (attrs) {
                /// <summary>
                /// Updates infobar
                /// </summary>
                /// <param name="attrs"></param>

                if (typeof attrs.id != 'undefined') {
                    $("#blueprint_id", this.infobar).html("Id: " + attrs.id);
                }
                if (typeof attrs.Name != 'undefined') {
                    $("#blueprint_name", this.infobar).html(attrs.Name);
                }
                if (typeof attrs.Description != 'undefined') {
                    $("#blueprint_desc", this.infobar).html(attrs.Description);
                }
                if (typeof attrs.rectanglesCount != 'undefined') {
                    $("#rect_count", this.infobar).html("Rectangles count: " + rectanglesCount);
                }
                if (typeof attrs.ellipsesCount != 'undefined') {
                    $("#ellipse_count", this.infobar).html("Ellipses count: " + attrs.ellipsesCount);
                }
            }
        });

        return BlueprintView;
    });