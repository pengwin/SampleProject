define([
  'views/graphics/canvasgraphics',
  'jquery',
  'qunit',
], function (CanvasGraphics, $) {

    var testCanvasGraphics = function () {

        QUnit.module("CanvasGraphics Tests");


        QUnit.test("constructor test", function () {
            var target = new CanvasGraphics();

            QUnit.equal(target.width, 1, 'default width test');
            QUnit.equal(target.height, 1, 'default height test');
            QUnit.equal(target.style.color, 'white', 'default color test');
            QUnit.equal(target.padding, 5, 'default padding test');
            QUnit.ok(typeof target._grid != 'undefined', 'grid has been created');
        });

        QUnit.test("render test", function () {
            var target = new CanvasGraphics();

            QUnit.ok(typeof target.el != 'undefined', 'dom element has been created');
            QUnit.ok(typeof target._paper != 'undefined', 'paper has been created');
            QUnit.equal($("svg", target.el).length, 1, 'svg has been created');
            QUnit.equal($("svg #grid", target.el).length, 1, 'grid has been rendered');
        });

        QUnit.test("set test", function () {
            var target = new CanvasGraphics();

            target.set({ width: 250 });
            target.set({ height: 300 });

            // test div size
            QUnit.equal($(target.el).width(), 250, "test set width: div width");
            QUnit.equal($(target.el).height(), 300, "test set height: div height");

            // test svg size
            QUnit.equal($("svg", target.el).attr('width'), 250, "test set width: svg width");
            QUnit.equal($("svg", target.el).attr('height'), 300, "test set height: svg height");


            target.set({ padding: 20 });
            QUnit.equal($(target.el).css('padding-left'), '20px', "test set padding: css padding-left test");
            QUnit.equal($(target.el).css('padding-top'), '20px', "test set padding: css padding-topt test");
        });

        QUnit.test("setStyle test", function () {
            var target = new CanvasGraphics();

            target.setStyle({ color: '#efefef' });

            QUnit.equal($(target.el).css('background-color'), "rgb(239, 239, 239)", "test setcolor");

            target.setStyle({ gridColor: '#efefef' });

            QUnit.equal(target._grid._el.attr('stroke'), '#efefef', "test set gridColor");
        });

        QUnit.asyncTest("mousemove async test", function () {
            QUnit.expect(1);
            var target = new CanvasGraphics();

            target.on('dom:mousemove', function (sender, curpos) {
                QUnit.ok(true, "mouse move was called");
            });

            $(target.el).trigger('mousemove'); // simulate mousemove

            setTimeout(function () {
                QUnit.start();
            }, 100);
        });

        QUnit.test("drawRectangle test", function () {
            var target = new CanvasGraphics();
            var actual = target.drawRectangle();

            QUnit.ok(typeof actual != 'undefined', 'element has been returned');
            QUnit.equal($("svg rect", target.el).length, 2, "rect has been added to paper");
        });
    };
    return testCanvasGraphics;
});