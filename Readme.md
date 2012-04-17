Intro
====================

Requirements:
-------------
 * VS 2010 SP1
 * ASP.NET MVC 3

 Frontend:
 ---------
* Design: twitter bootstrap
* Framework: Backbone
* Template rendering: Underscore
* Module loader: require.js
* SVG graphics: Raphael
* jquery

Backend:
--------
* Framework: Asp.net MVC 3
* ORM: Entity framework 4.1 Code first
* Database: SqlCe 4.0
* OpenID authentication: DotNetOpenAuth 
* DI container: Ninject

What Does this app do?
---------------------
In general it is a simple picture editor. This pictures in app is called 'blueprints'. 
Authenticated with OpenID users can create, edit, clone, search and delete this blueprints.
Every user has an unique api key. This key is used for users authorization in REST-like service.
The frontend app editor manipulates blueprints on server with this REST-like service.
