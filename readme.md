# <a id="top"></a>RezRouting.Net

RezRouting sets up routes in ASP.Net web applications based on a flexible resource-oriented structure.

Features and benefits:

- ASP.Net MVC support (ASP.Net Web API coming soon!)
- Clean URLs, with a range of formatting options (lowercase, dashed, underscored etc.)
- Suitable for both user-facing web applications and APIs designed for machine-to-machine communication
- Support for singular resources, collections and various nested combinations, e.g. /products/27229/reviews
- Partition actions for a resource into separate controllers - no more bloated controllers and SRP violations
- Built-in CRUD routing scheme - similar to Ruby on Rails' [Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default)
- Built-in task-centric routing scheme - a flexible scheme for more complex applications
- An extensible route building mechanism that allows you to set up your own routing conventions

See [Thinking Resourcefully](#background) in the WIKI for further details on using a resource-centric URL structure.

## <a id="intro"></a>Introduction

ASP.Net web application frameworks like ASP.Net MVC and ASP.Net Web API offer a very simple route structure out-of-the-box. This doesn't scale well to more complex applications and doesn't provide a pattern for managing a hierarchy of resources. In addition, the controller-per-resource results in a large number of unrelated action methods on the same controller, resulting in bloated controllers.

RezRouting.Net provides a simple API for setting up resource-oriented routes in ASP.Net web applications. Here's some example code that you'd run at startup to map routes for a /session singular resource (used to manage sign-ins) and a /products collection resource, which, in turn contains a nested reviews collection:

```C#
public static void RegisterRoutes(RouteCollection routes)
{
	routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

	routes.MapRoute("Home", "", new {Controller = "Home", Action = "Index"});

	var mapper = new RouteMapper();
	var routeTypeBuilder = new CrudRouteTypeBuilder();
	mapper.RouteTypes(routeTypeBuilder.Build());
	mapper.Singular("Session", session => session.HandledBy<SessionController>());
	mapper.Collection("Products", products =>
	{
		products.HandledBy<ProductsController>();
		products.Items(product => product.HandledBy<ProductController>());
		products.Collection("Reviews", reviews => {
			reviews.HandledBy<ReviewsController>();
			reviews.Items(review => review.HandledBy<ReviewController>());
		});
	});
	new MvcRouteMapper().CreateRoutes(mapper.Build(), routes);
	UrlHelperExtensions.IndexRoutes(routes);
}
```

This would map web requests (based on HTTP method and URL path) to action methods on the specified controllers. Selected examples (not all) of the routes created are listed below:

- `GET /products` displays a list of products - ProductsController.Index action
- `GET /products/123` displays an individual product - ProductController.Show action
- `POST /products` creates a new product - ProductsController.Create action
- `GET /products/123/reviews` displays all reviews for a product collection item - ReviewsController.Index action

Setting up routes like this manually is tedious and error-prone. RezRouting allows you to define the structure of your application in terms of resources. It then identifies the types of route supported by each resource's controllers and registers the routes in a consistent format.

RezRouting was inspired by [Ruby on Rails Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default) and its ASP.Net MVC port [Restful Routing](http://restfulrouting.com/). RezRouting's built-in CRUD routing scheme structures routes are structured in the same way as those created by Ruby on Rails out-of-the box. See [Background - Thinking Resourcefully](#background) below for further background on this type of URL structure or skip ahead to [getting started](#getting-started)

##<a id="author">Author</a>
Dan Malcolm [@lakescoder](http://twitter.com/lakescoder) - [blog](http://www.danmalcolm.com)

##<a id="contributors">Contributors</a>
Your name here! Add a feature, help with the documentation - all contributions welcome.

##<a id="license">License</a>
RezRouting.Net is released under the MIT License. See the [bundled LICENSE](https://github.com/MehdiK/RezRouting.Net/blob/master/LICENSE) file for details.
