// FormView module

define([
  'jquery',
  '../../Lib/bootstrap/bootstrap-modal-amd',
  'underscore',
  'backbone',
  'text!templates/form.htm'
], function ($, $modal, _, Backbone, form_template) {

    /**
    * Form view
    * responsible for:
    *  rendering  form which generated according to data dictionary
    *  fetching data dictionary from rendered form
    */
    var FormView = Backbone.View.extend({
        initialize: function () {
            /// <summary>
            /// Constructor
            /// </summary>

        },
        render: function (caption, attrs, excludedAttrs) {
            /// <summary>
            /// Renders form with caption and fields with value from attrs
            /// </summary>
            /// <param name="caption"></param>
            /// <param name="attrs"></param>
            /// <param name="excludedAttributes">array of attributes which won't be rendered (optional)</param>

            this.el = this.make("div", { class: "hide modal" });
            var excludedFields = {};
            // create object from array
            // because I don't want to search excluded attribute 
            // in array every iteration in template generator
            if (typeof excludedAttrs != 'undefined') {
                for (var i = 0; i < excludedAttrs.length; i++) {
                    excludedFields[excludedAttrs[i]] = true;
                }
            }
            var context = { caption: caption, modelKeys: Object.keys(attrs), model: attrs, excludedFields: excludedFields };
            var compiledTemplate = _.template(form_template, context);
            $(this.el).html(compiledTemplate);
            this.submitButton = $('#submit', this.el);
            this.cancelButton = $('#cancel', this.el);
            return this.el;
        },

        fetch: function () {
            /// <summary>
            /// Gets data from form fields 
            /// </summary>
            /// <returns type="">fetched data</returns>

            if (typeof this.el == 'undefined') {
                throw { message: "form hasn't been rendered" };
            }
            var result = {};
            var content = $('div#form_content', this.el);
            $("input[type='text']", content).each(function (index, item) {
                var id = $(item).attr('id');
                var value = $(item).attr('value');
                // try to get number
                var floatVal = parseFloat(value);
                // if it's number
                if (!isNaN(floatVal)) {
                    result[id] = floatVal; // store number
                } else {
                    result[id] = value; // store without changes
                }
            });
            return result;
        },
        message: function (message) {
            /// <summary>
            /// Gets or sets message on form
            /// </summary>
            /// <param name="message"></param>

            if (typeof this.el == 'undefined') {
                throw { message: "form hasn't been rendered" };
            }
            var messageEl = $('#err_message', this.el);
            if (typeof message == 'undefined') {
                return messageEl.html();
            }
            messageEl.html(message);
        },

        show: function () {
            /// <summary>
            /// Shows form
            /// </summary>

            if (typeof this.el == 'undefined') {
                throw { message: "form hasn't been rendered" };
            }
            window.jQuery(this.el).modal('show');
        },

        hide: function () {
            /// <summary>
            /// Hides form
            /// </summary>

            if (typeof this.el == 'undefined') {
                throw { message: "form hasn't been rendered" };
            }
            window.jQuery(this.el).modal('hide');
        },

        update: function (attrs) {
            /// <summary>
            /// Updates form fields with members of data
            /// </summary>
            /// <param name="attrs"></param>

            if (typeof this.el == 'undefined') {
                throw { message: "form hasn't been rendered" };
            }
            if (typeof attrs == 'undefined') {
                throw { message: "parameter 'attrs' not defined" };
            }

            var content = $('div#form_content', this.el);

            $("input[type='text']", content).each(function (index, item) {
                var id = $(item).attr('id');
                $(item).attr('value', attrs[id]);
            });
        }

    });
    return FormView;
});