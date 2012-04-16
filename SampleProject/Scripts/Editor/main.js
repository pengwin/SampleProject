// Require.js allows us to configure shortcut alias
// Their usage will become more apparent further along in the tutorial.
require.config({
    paths: {
        // Major libraries
        jquery: '../Lib/jQuery/jquery-1.7.1',
        underscore: '../Lib/Underscore/underscore-amd',
        backbone: '../Lib/Backbone/backbone-amd',
        eve: '../Lib/Raphael/eve',
        raphael: '../Lib/Raphael/raphael',

        // Require.js plugins
        text: '../Lib/require/text',
        order: '../Lib/require/order'

        // Just a short cut so we can put our html outside the js dir
        // When you have HTML/CSS designers this aids in keeping them out of the js directory
        //templates: '../templates'
    }
});

// Let's kick off the application

require([
  'app',
  'backbone'
], function (AppController,Backbone) {

    var App = new AppController();
    Backbone.history.start();

});