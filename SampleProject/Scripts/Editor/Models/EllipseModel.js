define([
  'jquery',
  'underscore',
  'backbone',
  'models/elementmodel'
], function ($, _, Backbone, ElementModel) {

    /**
    * Model of ellipse block
    */
    var EllipseModel = ElementModel.extend({

        defaults: {
            type: 'ellipse',
            x: 0,
            y: 0,
            width: 10,
            height: 10
        }
    });
    return EllipseModel;
});