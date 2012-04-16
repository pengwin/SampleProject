// Use this as a quick template for future modules
define([
  'jquery',
  'underscore',
  'backbone',
], function ($, _, Backbone) {
    var MoveAction = function (model) {
    	/// <summary>
        /// Represents moving action
    	/// performs actions on model
    	/// </summary>
    	/// <param name="model"></param>

        this.model = model;

        var self = this;

        this.pos = { x: 0, y: 0 };
        this.lastPos = { x: 0, y: 0 };

        this.state = 'no_initial_pos';

    };

    MoveAction.prototype.setPosition = function (position) {
    	/// <summary>
    	/// Set position of cursor
    	/// </summary>
        /// <param name="position"></param>
        
        this.lastPos.x = this.pos.x;
        this.lastPos.y = this.pos.y;

        this.pos.x = position.x;
        this.pos.y = position.y;
        if (this.state == 'moving') {

            var data = this.model.toJSON();
            var deltaX = this.pos.x - this.lastPos.x;
            var deltaY = this.pos.y - this.lastPos.y;
            this.model.set({
                x: data.x + deltaX,
                y: data.y + deltaY
            });
        }
    };

    MoveAction.prototype.execute = function () {
    	/// <summary>
        /// performs action
        /// first execution starts moving,
    	/// second execution ends moving
    	/// </summary>
        /// <returns type=""></returns>
        
        if (this.state == 'no_initial_pos') {
            this.state = 'moving';
        } else if (this.state == 'moving') {
            this.state = 'no_initial_pos';
        }
        return true;
    };

    return MoveAction;
});