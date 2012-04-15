
define([
  'jquery',
  'underscore',
  'backbone',
  'models/elementmodel'
], function ($, _, Backbone, ElementModel) {

    /**
    * Model of rectangle block
    */
    var RectangleModel = ElementModel.extend({

        defaults: {
            type: 'rectangle',
            x: 0,
            y: 0,
            width: 10,
            height: 10
        }
    });
    return RectangleModel;
});
