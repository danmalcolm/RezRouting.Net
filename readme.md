# <a id="top"></a>RezRouting.Net

RezRouting.Net configures routes for ASP.Net web applications using  [resource-oriented](wiki/01. Background - Thinking-Resourcefully.md) URLs.

Structure your application around clean, beautiful URLs based on:

* Collections: /users, /products, /orders
* Collection items: /users/dave, /orders/000366
* Singular resources: /session
* Nested combinations of the above: /users/phoebe/logins, /orders/000366/dispatches

Other features:

- Routing conventions automatically map routes based on actions supported by each resource's  controllers
- Use CRUD route conventions for simpler applications - similar to Ruby on Rails' [Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default)
- Use task-centric route conventions ( /orders/000366/cancel, /orders/000366/approve ) - a flexible scheme for more complex user-facing web applications (because you never really just "edit" stuff...)
- Partition actions for a resource into separate controllers - goodbye bloated controllers!
- Numerous options for URL formatting (lowercase, hyphenated etc)

## Why?

Many applications would benefit from consistently structured routes with clean URLs - the resource-centric approach offers a flexible URL structure that suits a range of applications.

ASP.Net web application frameworks like ASP.Net MVC and ASP.Net Web API have a very simple "flat" route structure by default that encourages developers to add lots of unrelated operations to a single controller class and doesn't support a hierarchical route structure.

RezRouting was inspired by [Ruby on Rails Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default) and its ASP.Net MVC port [Restful Routing](http://restfulrouting.com/).

## Example

RezRouting configuration involves defining the structure of your application's resources, the routes that each resource supports and the controllers that handle the routes.

In this example (from the [ASP.Net MVC CRUD demo](https://github.com/danmalcolm/RezRouting.Net/tree/master/src/RezRouting.Demos.Crud)), we're using a convention that looks for CRUD-focussed controller actions to support the management of a collection of products and reviews:

```C#
var root = RootResourceBuilder.Create();
root.ApplyRouteConventions(new CrudRouteConventions());
root.Collection("Products", products =>
{
    products.HandledBy<ProductsController>();
    products.Items(product =>
    {
        product.HandledBy<ProductController>();
        product.Collection("Reviews", reviews =>
        {
            reviews.HandledBy<ReviewsController>();
            reviews.Items(review => review.HandledBy<ReviewController>());
        });
    });
});
root.Singular("Session", session => session.HandledBy<SessionController>());
root.MapMvcRoutes(routes);
```

The CRUD route conventions map HTTP methods to CRUD operations. The following routes apply to session management:

|Route                           |HTTP Method & URL / Description |
|--------------------------------|--------------------------------------|--------------------------|
|Session.Show                    |GET /session<br/>Displays authentication status and sign in form if user not already logged in (SessionController.Index)  |
|Session.Create                  |POST /session<br/>Authenticates user (the sign in form posts to this action) (SessionController.Create) |
|Session.Delete                  |DELETE /session<br/>Signs out the user (SessionController.Delete) |

Here are a few examples of the routes set up to view and manage products and the reviews belonging to individual products:

|Route                           |HTTP Method & URL / Description |
|--------------------------------|--------------------------------------|--------------------------|
|Products.Index                  |GET /products<br/>Display a list of products (ProductsController.Index) |
|Products.Product.Show           |GET /products/{id}<br/>Displays an individual product (ProductController.Show) |
|Products.Product.Edit           |GET /products/{id}/edit<br/>Displays form to edit a product (ProductController.Edit) |
|Products.Product.Update         |PUT /products/{id}<br/>Saves changes to product (ProductController.Update)  |
|Products.Product.Delete         |DELETE /products/{id}<br/>Deletes a product (ProductController.Delete) |
|Products.Product.Reviews.Index  |GET /products/{productId}/reviews<br/> Displays all reviews for a given product (ReviewsController.Index) |
|Products.Product.Reviews.New    |GET /products/{productId}/reviews/new<br />Displays form for adding a review to a product (ReviewsController.New) |

## Getting Started

The [WIKI](./wiki) includes full background and documentation. Clone / fork the project to view end-to-end working demos.

## Contributions and Feedback

Request a feature or report a bug via GitHub issues.

##Author

Dan Malcolm [@lakescoder](http://twitter.com/lakescoder) - [blog](http://www.danmalcolm.com)

##License

RezRouting.Net is released under the MIT License. See the [bundled LICENSE](https://github.com/MehdiK/RezRouting.Net/blob/master/LICENSE) file for details.
# <a id="top"></a>RezRouting.Net

RezRouting.Net configures routes for ASP.Net web applications using  [resource-oriented](wiki/01. Background - Thinking-Resourcefully.md) URLs.

Structure your application around clean, beautiful URLs based on:

* Collections: /users, /products, /orders
* Collection items: /users/dave, /orders/000366
* Singular resources: /session
* Nested combinations of the above: /users/phoebe/logins, /orders/000366/dispatches

Other features:

- Routing conventions automatically map routes based on actions supported by each resource's  controllers
- Use CRUD route conventions for simpler applications - similar to Ruby on Rails' [Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default)
- Use task-centric route conventions ( /orders/000366/cancel, /orders/000366/approve ) - a flexible scheme for more complex user-facing web applications (because you never really just "edit" stuff...)
- Partition actions for a resource into separate controllers - goodbye bloated controllers!
- Numerous options for URL formatting (lowercase, hyphenated etc)

## Why?

Many applications would benefit from consistently structured routes with clean URLs - the resource-centric approach offers a flexible URL structure that suits a range of applications.

ASP.Net web application frameworks like ASP.Net MVC and ASP.Net Web API have a very simple "flat" route structure by default that encourages developers to add lots of unrelated operations to a single controller class and doesn't support a hierarchical route structure.

RezRouting was inspired by [Ruby on Rails Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default) and its ASP.Net MVC port [Restful Routing](http://restfulrouting.com/).

## Example

RezRouting configuration involves defining the structure of your application's resources, the routes that each resource supports and the controllers that handle the routes.

In this example (from the [ASP.Net MVC CRUD demo](https://github.com/danmalcolm/RezRouting.Net/tree/master/src/RezRouting.Demos.Crud)), we're using a convention that looks for CRUD-focussed controller actions to support the management of a collection of products and reviews:

```C#
var root = RootResourceBuilder.Create();
root.ApplyRouteConventions(new CrudRouteConventions());
root.Collection("Products", products =>
{
    products.HandledBy<ProductsController>();
    products.Items(product =>
    {
        product.HandledBy<ProductController>();
        product.Collection("Reviews", reviews =>
        {
            reviews.HandledBy<ReviewsController>();
            reviews.Items(review => review.HandledBy<ReviewController>());
        });
    });
});
root.Singular("Session", session => session.HandledBy<SessionController>());
root.MapMvcRoutes(routes);
```

The CRUD route conventions map HTTP methods to CRUD operations. The following routes apply to session management:

|Route                           |HTTP Method & URL / Description |
|--------------------------------|--------------------------------------|--------------------------|
|Session.Show                    |GET /session<br/>Displays authentication status and sign in form if user not already logged in (SessionController.Index)  |
|Session.Create                  |POST /session<br/>Authenticates user (the sign in form posts to this action) (SessionController.Create) |
|Session.Delete                  |DELETE /session<br/>Signs out the user (SessionController.Delete) |

Here are a few examples of the routes set up to view and manage products and the reviews belonging to individual products:

|Route                           |HTTP Method & URL / Description |
|--------------------------------|--------------------------------------|--------------------------|
|Products.Index                  |GET /products<br/>Display a list of products (ProductsController.Index) |
|Products.Product.Show           |GET /products/{id}<br/>Displays an individual product (ProductController.Show) |
|Products.Product.Edit           |GET /products/{id}/edit<br/>Displays form to edit a product (ProductController.Edit) |
|Products.Product.Update         |PUT /products/{id}<br/>Saves changes to product (ProductController.Update)  |
|Products.Product.Delete         |DELETE /products/{id}<br/>Deletes a product (ProductController.Delete) |
|Products.Product.Reviews.Index  |GET /products/{productId}/reviews<br/> Displays all reviews for a given product (ReviewsController.Index) |
|Products.Product.Reviews.New    |GET /products/{productId}/reviews/new<br />Displays form for adding a review to a product (ReviewsController.New) |

## Getting Started

The [WIKI](./wiki) includes full background and documentation. Clone / fork the project to view end-to-end working demos.

## Contributions and Feedback

Request a feature or report a bug via GitHub issues.

##Author

Dan Malcolm [@lakescoder](http://twitter.com/lakescoder) - [blog](http://www.danmalcolm.com)

##License

RezRouting.Net is released under the MIT License. See the [bundled LICENSE](https://github.com/MehdiK/RezRouting.Net/blob/master/LICENSE) file for details.
