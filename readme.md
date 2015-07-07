# <a id="top"></a>RezRouting.Net

RezRouting configures routes for ASP.Net MVC web applications using clean [resource-oriented](wiki/01. Background - Thinking-Resourcefully.md) URLs.

"Resourceful" URLs identify a web application's resources using a simple path structure, for example:

* /products - get a list of products (a collection resource)
* /products/1234 - get an individual product (an item resource)
* /products/1234/reviews - get the reviews belonging to a product resource (a nested collection resource)
* /profile/sessions - get a history of the currently signed-in user's recent sessions

This type of URL structure fits a wide range of scenarios and is in use on many sites today.

## Why?

ASP.Net web application frameworks like ASP.Net MVC and ASP.Net Web API have a very simple "flat" route structure by default. This doesn't support a hierarchy of resources or scale easily to more complex applications. It also encourages developers to add lots of unrelated operations to a single controller class. There's also a good chance that the URLs generated from these routes will include capital letters, which look horrible.

RezRouting's route configuration API makes it easy to define a hierarchy of resources within your application and the routes that apply to them.

Features include:

- Routing conventions automatically map routes based on actions supported by each resource's controllers
- Use CRUD route conventions for simpler applications - similar to Ruby on Rails' [Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default)
- Use task-centric route conventions ( /orders/000366/cancel, /orders/000366/approve ) - a flexible scheme for more complex user-facing web applications (because you never really just "edit" stuff...)
- Partition actions for a resource into separate controllers - goodbye bloated controllers!
- Numerous options for URL formatting (lowercase, hyphenated etc)

## Example

RezRouting configuration involves defining the structure of your application's resources, the routes that each resource supports and the controllers that handle the routes.

In this example (from the [ASP.Net MVC CRUD demo](https://github.com/danmalcolm/RezRouting.Net/tree/master/src/RezRouting.Demos.Crud)), we're using a convention that looks for CRUD-focussed controller actions to support the management of a collection of products and reviews:

```C#
var root = RootResourceBuilder.Create();
root.Singular("Session", session => session.HandledBy<SessionController>());
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
root.ApplyRouteConventions(new CrudRouteConventions());
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

## Thanks

RezRouting was inspired by [Ruby on Rails Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default) and its ASP.Net MVC port [Restful Routing](http://restfulrouting.com/).

## Author

Dan Malcolm [@lakescoder](http://twitter.com/lakescoder) - [blog](http://www.danmalcolm.com)

##License

RezRouting.Net is released under the MIT License. See the [bundled LICENSE](https://github.com/MehdiK/RezRouting.Net/blob/master/LICENSE) file for details.
