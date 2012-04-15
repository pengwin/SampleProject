//Blueprint Model module

define([
        'jquery',
        'underscore',
        'backbone',
        'models/elementmodel',
        'models/canvasmodel'
    ], function ($, _, Backbone,ElementModel,CanvasModel) {

        // Collection of blueprint elements
        var Elements = Backbone.Collection.extend({
            model: ElementModel
        });

        // Blueprint model
        // represents data from server and for server
        var BlueprintModel = Backbone.Model.extend({
            urlRoot: '/Ajax/Blueprint/',
            
            defaults: {
                Name: '',
                Description: '',
                JsonData: "{}"
            },

            initialize: function () {
            	/// <summary>
            	/// Constructor.
            	/// </summary>
                this.canvas = new CanvasModel();
                this.elements = new Elements();
            },

            validate: function (attrs) {
            	/// <summary>
            	/// Validates incoming attributes and return validation error
            	/// </summary>
                if ((typeof attrs.Name == 'undefined') || (attrs.Name == '')) {
                    return "Name is required";
                }
            },

            parseJsonData: function (jsonData) {
            	/// <summary>
            	/// Parses json data from server and updates inner data with it.
            	/// </summary>
                try {
                    var data = JSON.parse(jsonData);
                    if (typeof data.canvas != 'undefined') {
                        this.canvas.set(data.canvas);
                    }
                    if (typeof data.elements != 'undefined' && data.elements.constructor == Array) {
                        this.elements.reset(data.elements);
                    }
                } catch (e) {
                    console.log("Blueprint JsonData parsing error: " + e);
                }
            },
            
            parse: function (attrs) {
            	/// <summary>
            	/// Parses data from server
            	/// </summary>
                if ((typeof attrs.JsonData != 'undefined') && (attrs.JsonData != '')) {
                    this.parseJsonData(attrs.JsonData);
                }
                attrs.JsonData = "{}";
                return attrs;
            },

            toJSON: function () {
            	/// <summary>
            	/// Gets hash table of attributes.
            	/// </summary>
                var result = Backbone.Model.prototype.toJSON.call(this);

                var jsonData = {};
                var canvas = this.canvas.toJSON();
                jsonData.canvas = canvas;
                var elements = this.elements.toJSON();
                jsonData.elements = elements;

                result.JsonData = JSON.stringify(jsonData);
                return result;
            }
        });
        return BlueprintModel;
    });