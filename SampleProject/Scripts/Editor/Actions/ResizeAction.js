// Module: DrawRectAction

define([
  'jquery',
  'underscore',
  'backbone'
], function ($, _, Backbone) {

    var ResizeAction = function (model) {
    	/// <summary>
        /// Class represents a resize action
        /// Modifies the model
    	/// </summary>
    	/// <param name="blueprintWidget"></param>

        this.model = model;

        this.pos = { x: 0, y: 0 };

        this.state = 'no_rect';

    };

    ResizeAction.prototype.setPosition = function (position) {
    	/// <summary>
        /// Sets the cursor position.
    	/// Resizes current rectangle if action in resize rect state.
    	/// </summary>
        /// <param name="position"></param>
        
        this.pos.x = position.x;
        this.pos.y = position.y;
        if (this.state == 'resize_rect') {
            var newWidth = this.pos.x - this.startPos.x;
            var newHeight = this.pos.y - this.startPos.y;
            this.model.set({
                width: newWidth,
                height: newHeight
            });
        }
    };

    ResizeAction.prototype.execute = function () {
    	/// <summary>
        /// Performs the action.
        /// First call creates rectangle widget, sets it as current and sets rect resize state.
    	/// Second call puts action to initial state.
    	/// </summary>
    	/// <returns type=""></returns>

        if (this.state == 'no_rect') {
            this.startPos = {
                x: this.model.get('x'),
                y: this.model.get('y')
            };
            this.state = 'resize_rect';
        } else if (this.state == 'resize_rect') {
            this.state = 'no_rect';
        }
        return true;
    };

    return ResizeAction;
});